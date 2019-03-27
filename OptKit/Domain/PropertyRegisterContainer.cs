using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace OptKit.Domain
{
    class PropertyRegisterContainer
    {
        List<Property> _properties { get; } = new List<Property>();
        readonly object _compileLock = new object();
        static readonly object _globalIndexLock = new object();
        static readonly object _containersLock = new object();
        static int _nextGlobalIndex;
        static PropertyRegisterContainer _lastCached;
        static Dictionary<Type, PropertyRegisterContainer> _containers = new Dictionary<Type, PropertyRegisterContainer>();

        public static void RegisterProperty(Property property)
        {
            if (property.GlobalIndex >= 0) { throw new InvalidOperationException("同一个属性只能注册一次。"); }

            lock (_globalIndexLock)
                property.GlobalIndex = _nextGlobalIndex++;

            var container = GetOrCreateRegisterContainer(property.OwnerType);
            container.AddProperty(property);
        }

        public static PropertyRegisterContainer GetOrCreateRegisterContainer(Type ownerType)
        {
            //由于经常是对同一类型的实体进行大量的构造操作，所以这里对最后一次使用的类型进行缓存
            if (_lastCached?.OwnerType == ownerType) { return _lastCached; }
            PropertyRegisterContainer container = null;
            if (!_containers.TryGetValue(ownerType, out container))
            {
                lock (_containersLock)
                {
                    if (!_containers.TryGetValue(ownerType, out container))
                    {
                        container = new PropertyRegisterContainer(ownerType);
                        var baseType = ownerType.BaseType;
                        if (baseType != typeof(Entity))
                        {
                            if (baseType == typeof(object))
                                throw new InvalidProgramException(string.Format("属性所属类型 {0} 必须继承自 Entity 类。", ownerType));
                            container.BaseType = GetOrCreateRegisterContainer(baseType);
                        }
                        _containers.Add(ownerType, container);
                    }
                }
            }
            _lastCached = container;
            return container;
        }

        public PropertyRegisterContainer(Type ownerType)
        {
            OwnerType = ownerType;
        }

        public Type OwnerType { get; }

        public PropertyRegisterContainer BaseType { get; set; }

        PropertyContainer _container;

        public PropertyContainer Container
        {
            get
            {
                if (_container == null)
                {
                    lock (_compileLock)
                    {
                        if (_container == null)
                        {
                            RunPropertyResigtry(OwnerType);
                             RunClrPropertyResigtry(OwnerType);
                            var container = new PropertyContainer(this);
                            container.CompileProperties();
                            _container = container;
                        }
                    }
                }
                return _container;
            }
        }
        public static void RunPropertyResigtry(Type entityType)
        {
            //泛型类在绑定具体类型前，是无法初始化它的静态字段的，所以这里直接退出，而留待子类来进行初始化。
            if (entityType.ContainsGenericParameters && !entityType.IsAbstract)
                throw new InvalidOperationException("声明属性的泛型类型 {0}，必须声明为 abstract，否则无法正常注册属性！".FormatArgs(entityType.FullName));

            //同时运行基类及它本身的所有静态构造函数
            var types = entityType.GetHierarchy(typeof(Entity), typeof(object)).ToArray();
            for (int i = types.Length - 1; i >= 0; i--)
            {
                var type = types[i];
                System.Runtime.CompilerServices.RuntimeHelpers.RunClassConstructor(type.TypeHandle);
            }
        }

        static void RunClrPropertyResigtry(Type entityType)
        {
            entityType = OptKit.ComponentModel.TrackableFactory.GetRawType(entityType);
            var properties = entityType.GetProperties(BindingFlags.Instance | BindingFlags.Public);
            foreach (var property in properties)
            {
                if (!property.GetMethod.IsFinal && property.GetMethod.IsVirtual)
                {
                    var container = GetOrCreateRegisterContainer(property.DeclaringType);
                    if (!container.Properties.Any(p => p.OwnerType == property.DeclaringType && p.PropertyName == property.Name))
                    {
                        var pt = typeof(ValueProperty<>).MakeGenericType(property.PropertyType);
                        var p = Activator.CreateInstance(pt) as Property;
                        p.OwnerType = property.DeclaringType;
                        p.DeclareType = property.DeclaringType;
                        p.PropertyName = property.Name;
                        p.PropertyType = property.PropertyType;
                        RegisterProperty(p);
                    }
                }
            }
        }

        public IReadOnlyCollection<Property> Properties
        {
            get { return _properties; }
        }

        void AddProperty(Property property)
        {
            _properties.Add(property);
        }
    }
}
