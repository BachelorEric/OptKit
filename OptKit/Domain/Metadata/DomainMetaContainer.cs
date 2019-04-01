using OptKit.Data.SqlTree;
using OptKit.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace OptKit.Domain.Metadata
{
    public class DomainMetaContainer
    {
        Dictionary<Type, DomainMeta> _metas = new Dictionary<Type, DomainMeta>();
        readonly object _syncLock = new object();
        Dictionary<Type, List<DomainConfig>> _typeConfigs = new Dictionary<Type, List<DomainConfig>>(100);

        public static event EventHandler<DomainMeta> MetaCreated;

        protected void OnMetaCreated(DomainMeta meta)
        {
            MetaCreated?.Invoke(this, meta);
        }

        bool _allEntitiesLoaded;
        public void EnsureAllLoaded()
        {
            if (!_allEntitiesLoaded)
            {
                foreach (var module in RT.GetModules())
                {
                    foreach (var type in module.Assembly.GetTypes())
                    {
                        if (IsAggregationRoot(type))
                            Find(type);
                    }
                }
                _allEntitiesLoaded = true;
            }
        }

        /// <summary>
        /// 查询某个实体类型所对应的实体信息。查询不到，就报出异常。
        /// </summary>
        /// <param name="entityType"></param>
        /// <returns></returns>
        public DomainMeta Get(Type type)
        {
            var meta = Find(type);
            if (meta == null) throw new InvalidProgramException("没有找到类型[{0}]的元数据，请检查是否实现IDomain接口".FormatArgs(type.GetQualifiedName()));
            return meta;
        }

        public DomainMeta Find(Type type)
        {
            if (!typeof(IDomain).IsAssignableFrom(type))
                return null;
            DomainMeta meta;
            if (!_metas.TryGetValue(type, out meta))
            {
                lock (_syncLock)
                {
                    if (!_metas.TryGetValue(type, out meta))
                    {
                        meta = Create(type);
                        _metas[type] = meta;
                    }
                }
            }
            return meta;
        }

        DomainMeta Create(Type type)
        {
            if (IsAggregationRoot(type))
            {
                return CreateMetaRecur(type);
            }
            else
            {
                //如果不是根类型，则应该先递归生成该根类型的整个组合元数据。
                //这样，其中的子类型也就生成好了。这时再进行查询。
                var rootType = GetRootType(type);
                if (rootType != null)
                {
                    CreateMetaRecur(rootType);
                    DomainMeta meta;
                    if (_metas.TryGetValue(type, out meta))
                        return meta;
                }
            }
            return null;
        }

        Type GetRootType(Type type)
        {
            var root = type;
            while (root != null && !IsAggregationRoot(root))
                root = GetParentType(root);
            return root;
        }

        Type GetParentType(Type type)
        {
            var container = DomainManager.GetPropertyContainer(type);
            var parentProperty = container.DataProperties.OfType<IRefIdProperty>().FirstOrDefault(p => p.ReferenceType == ReferenceType.Parent);
            if (parentProperty != null) { return parentProperty.RefEntityProperty.PropertyType; }
            return null;
        }

        bool IsAggregationRoot(Type type)
        {
            var attr = type.GetCustomAttribute<DomainAttribute>();
            return attr == null || attr.Category == DomainCategory.Root;
        }

        DomainMeta CreateMetaRecur(Type type, DomainMeta parentMeta = null)
        {
            var meta = new DomainMeta { DomainType = type };
            meta.Caption = type.GetCustomAttribute<CaptionAttribute>()?.Caption ?? type.Name;
            meta.Attribute = type.GetCustomAttribute<DomainAttribute>() ?? new DomainAttribute();
            //聚合关系设置
            if (parentMeta != null)
            {
                meta.Parent = parentMeta;
                parentMeta.ChildrenInternal.Add(meta);
            }
            if (typeof(IEntity).IsAssignableFrom(type))//实体才需要映射表
            {
                var table = type.GetCustomAttribute<TableAttribute>();
                if (table != null)
                {
                    if (table.SqlView.IsNotEmpty())
                        meta.TabelMeta = new TableMeta { SqlView = table.SqlView };
                    else if (table.SqlViewType != null)
                    {
                        var sqlView = Activator.CreateInstance(table.SqlViewType) as ISqlView;
                        if (sqlView != null)
                            meta.TabelMeta = new TableMeta { QueryView = () => sqlView.GetSqlView() as ISqlSelect };
                    }
                    else
                    {
                        meta.TabelMeta = new TableMeta { TableName = table.TableName ?? meta.DomainType.Name };
                    }
                }
            }

            CreatePropertyMeta(meta);
            Config(meta);
            OnMetaCreated(meta);
            return meta;
        }
        void Config(DomainMeta meta)
        {
            var hierachy = meta.DomainType.GetHierarchy(typeof(DomainObject)).Reverse();
            foreach (var type in hierachy)
            {
                List<DomainConfig> configList = null;
                if (_typeConfigs.TryGetValue(type, out configList))
                {
                    var orderedList = configList.OrderBy(o => o.SetupIndex).ThenBy(o => o.InheritanceCount);
                    foreach (var config in orderedList)
                    {
                        config.ConfigMeta(meta);
                    }
                }
            }
        }

        internal void InitConfig(ModuleAssembly module, Type type)
        {
            if (!type.IsGenericTypeDefinition && !type.IsAbstract && type.IsSubclassOf(typeof(DomainConfig)))
            {
                var config = Activator.CreateInstance(type) as DomainConfig;
                config.SetupIndex = module.Index;
                config.InheritanceCount = type.GetHierarchy().Count();
                List<DomainConfig> typeList;
                if (!_typeConfigs.TryGetValue(config.DomainType, out typeList))
                {
                    typeList = new List<DomainConfig>(2);
                    _typeConfigs.Add(config.DomainType, typeList);
                }
                typeList.Add(config);
            }
        }

        void CreatePropertyMeta(DomainMeta meta)
        {
            var container = DomainManager.GetPropertyContainer(meta.DomainType);
            var attributes = GetPropertyAttributes(meta.DomainType);
            foreach (var property in container.Properties.Where(p => p.OwnerType == p.DeclareType && !(p is IListProperty) && !(p is IRefEntityProperty)))
            {
                Attribute[] attr;
                attributes.TryGetValue(property.Name, out attr);
                var p = CreatePropertyMeta(property, meta, attr);
                p.Owner = meta;
                meta.PropertiesInternal.Add(p);
            }
            foreach (var group in container.Properties.Where(p => p.OwnerType != p.DeclareType && !(p is IListProperty) && !(p is IRefEntityProperty)).GroupBy(p => p.DeclareType))
            {
                var typeAttributes = GetPropertyAttributes(group.Key);
                foreach (var property in group)
                {
                    Attribute[] attr;
                    typeAttributes.TryGetValue(property.Name, out attr);
                    var p = CreatePropertyMeta(property, meta, attr);
                    meta.PropertiesInternal.Add(p);
                }
            }
            foreach (var property in container.Properties.Where(p => p.OwnerType == p.DeclareType && p is IListProperty))
            {
                Attribute[] attr;
                attributes.TryGetValue(property.Name, out attr);
                var p = CreateChildPropertyMeta((IListProperty)property, meta, attr);
                meta.ChildPropertiesInternal.Add(p);
            }
            foreach (var group in container.Properties.Where(p => p.OwnerType != p.DeclareType && p is IListProperty).GroupBy(p => p.DeclareType))
            {
                var typeAttributes = GetPropertyAttributes(group.Key);
                foreach (var property in group)
                {
                    Attribute[] attr;
                    typeAttributes.TryGetValue(property.Name, out attr);
                    var p = CreateChildPropertyMeta((IListProperty)property, meta, attr);
                    meta.ChildPropertiesInternal.Add(p);
                }
            }
            meta.PropertiesInternal.TrimExcess();
            meta.ChildPropertiesInternal.TrimExcess();
        }

        ChildPropertyMeta CreateChildPropertyMeta(IListProperty property, DomainMeta domainMeta, Attribute[] attributes)
        {
            var item = new ChildPropertyMeta
            {
                Owner = domainMeta,
                Property = property,
                Attributes = attributes ?? new Attribute[0]
            };
            var childType = property.ItemType;
            if (childType != domainMeta.DomainType)
            {
                DomainMeta meta;
                if (_metas.TryGetValue(childType, out meta))
                    item.ChildMeta = meta;
                else
                {
                    item.ChildMeta = CreateMetaRecur(childType, domainMeta);
                    _metas.Add(childType, item.ChildMeta);
                }
            }
            else
                item.ChildMeta = domainMeta;
            return item;
        }

        DataPropertyMeta CreatePropertyMeta(IProperty property, DomainMeta domainMeta, Attribute[] attributes)
        {
            DataPropertyMeta meta = null;
            if (property is IRefIdProperty)
                meta = new RefPropertyMeta { Owner = domainMeta, Property = property };
            else
                meta = new DataPropertyMeta { Owner = domainMeta, Property = property };
            meta.Attributes = attributes ?? new Attribute[0];
            meta.Caption = meta.Attributes.OfType<CaptionAttribute>().FirstOrDefault()?.Caption;
            if (meta.Caption.IsNotEmpty())
                meta.Caption = meta.Attributes.OfType<PropertyAttribute>().FirstOrDefault()?.Label;
            var column = meta.Attributes.OfType<ColumnAttribute>().FirstOrDefault();
            if (column != null && !(property is ICaculateProperty))
            {
                meta.ColumnMeta = new ColumnMeta
                {
                    ColumnName = column.ColumnName,
                    DbType = column.DbType,
                    DbTypeLength = column.DbTypeLength,
                    DefaultValue = column.DefaultValue,
                    HasForeignKey = column.HasForeignKey,
                    IsIndexer = column.IsIndexer,
                    IsInsertable = column.IsInsertable,
                    IsNullable = column.IsInsertable,
                    IsPrimaryKey = column.IsPrimaryKey,
                    IsUpdateable = column.IsUpdateable,
                    UseSequence = column.UseSequence,
                    ViewPath = (property as IViewProperty)?.ViewPath
                };
            }
            return meta;
        }

        Dictionary<string, Attribute[]> GetPropertyAttributes(Type type)
        {
            var result = new Dictionary<string, Attribute[]>();
            var fields = type.GetFields(BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.FlattenHierarchy);
            foreach (var field in fields)
            {
                var p = field.GetValue(null) as IProperty;
                if (p != null)
                    result[p.Name] = field.GetCustomAttributes().ToArray();
            }
            var properties = type.GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.GetProperty);
            foreach (var property in properties)
            {
                if (!property.GetMethod.IsFinal && property.GetMethod.IsVirtual)
                    result.Add(property.Name, property.GetCustomAttributes().ToArray());
            }
            return result;
        }
    }
}
