using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OptKit.Domain
{
    class PropertyContainer : IPropertyContainer
    {
        PropertyRegisterContainer _container;
        Dictionary<string, IProperty> _properties = new Dictionary<string, IProperty>();
        List<IProperty> _dataProperties = new List<IProperty>();

        public PropertyContainer(PropertyRegisterContainer container)
        {
            _container = container;
        }

        public Type OwnerType { get { return _container.OwnerType; } }

        public IReadOnlyCollection<IProperty> Properties { get { return _properties.Values; } }

        public IReadOnlyCollection<IProperty> DataProperties { get { return _dataProperties; } }

        public IProperty Find(string propertyName, bool ignoreCase = false)
        {
            if (ignoreCase)
            {
                foreach (var key in _properties.Keys)
                {
                    if (key.CIEquals(propertyName))
                        return _properties[key];
                }
                return null;
            }
            IProperty result = null;
            _properties.TryGetValue(propertyName, out result);
            return result;
        }

        internal void CompileProperties()
        {
            var hierarchy = GetHierarchyContainers();
            int compiledIndex = 0;
            //从基类到子类
            for (int index = hierarchy.Count - 1; index >= 0; index--)
            {
                hierarchy[index].Properties.OrderBy(p => p.Name).ForEach(p =>
                {
                    if (p.CompiledIndex == -1)
                    {
                        bool isCaculated = p is ICaculateProperty;
                        IProperty overrided;
                        if (_properties.TryGetValue(p.Name, out overrided))
                        {
                            var x = overrided is ICaculateProperty;
                            if (x != isCaculated)
                                throw new AppException("[{0}]{1}属性[{2}]不能重写[{3}]{4}属性".FormatArgs(p.DeclareType.GetQualifiedName(), isCaculated ? "计算" : "非计算", p.Name, overrided.DeclareType.GetQualifiedName(), x ? "计算" : "非计算"));
                            if (overrided.OwnerType == p.OwnerType)
                                throw new AppException("[{0}]已存在属性[{1}],不能重复注册,声明类型[{2}]".FormatArgs(overrided.OwnerType.GetQualifiedName(), overrided.Name, p.DeclareType.GetQualifiedName()));
                            if (!isCaculated)
                                p.CompiledIndex = overrided.CompiledIndex;
                        }
                        else
                        {
                            if ((!isCaculated))
                            {
                                p.CompiledIndex = compiledIndex++;
                            }
                        }
                    }
                    if (p.CompiledIndex == compiledIndex)
                    {
                        compiledIndex++;
                    }
                    _properties[p.Name] = p;
                });
            }
            _dataProperties.AddRange(_properties.Values.Where(p => !(p is ICaculateProperty)));
            _dataProperties.TrimExcess();
        }

        List<PropertyRegisterContainer> GetHierarchyContainers()
        {
            var current = _container;
            var result = new List<PropertyRegisterContainer>();
            do
            {
                result.Add(current);
                current = current.BaseType;
            } while (current != null);
            return result;
        }
    }
}

