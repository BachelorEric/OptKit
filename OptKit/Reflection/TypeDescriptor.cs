using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OptKit.Reflection
{
    public class TypeDescriptor
    {
        static Dictionary<Type, PropertyDescriptorCollection> _typeDescriptorCache = new Dictionary<Type, PropertyDescriptorCollection>();
        static object _propertyDescriptorLock = new object();

        static PropertyDescriptor FindPropertyDescriptor(object obj, string property)
        {
            var type = obj.GetType();
            PropertyDescriptorCollection descriptors = null;
            if (!_typeDescriptorCache.TryGetValue(type, out descriptors))
            {
                lock (_propertyDescriptorLock)
                {
                    if (!_typeDescriptorCache.TryGetValue(type, out descriptors))
                    {
                        descriptors = System.ComponentModel.TypeDescriptor.GetProperties(obj);
                        _typeDescriptorCache.Add(type, descriptors);
                    }
                }
            }
            return descriptors[property];
        }

        public static void SetValue(object obj, string property, object value)
        {
            Check.NotNull(obj, nameof(obj));
            var descriptor = FindPropertyDescriptor(obj, property);
            if (descriptor == null)
                throw new MissingMemberException(obj.GetType().Name, nameof(property));
            descriptor.SetValue(obj, value);
        }

        public static object GetValue(object obj, string property)
        {
            Check.NotNull(obj, nameof(obj));
            var descriptor = FindPropertyDescriptor(obj, property);
            if (descriptor == null)
                throw new MissingMemberException(obj.GetType().Name, property);
            return descriptor.GetValue(obj);
        }
    }
}
