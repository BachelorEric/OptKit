using System;
using System.Collections.Generic;
using System.Text;

namespace OptKit.Domain.Data
{
    [Serializable]
    class FieldData : IFieldData
    {
        object[] _values;

        public FieldData(IPropertyContainer container)
        {
            _values = new object[container.DataProperties.Count];
        }

        void CheckProperty(IProperty property)
        {
            var index = property.CompiledIndex;
            if (index < 0 || index >= _values.Length)
                throw new IndexOutOfRangeException("属性[{0}]CompiledIndex值[{1}]超索引范围".FormatArgs(property.PropertyName, index));
        }

        public bool Exists(IProperty property)
        {
            CheckProperty(property);
            return _values[property.CompiledIndex] != null;
        }

        public object Get(IProperty property)
        {
            CheckProperty(property);
            var value = _values[property.CompiledIndex];
            if (value == DBNull.Value)
                return null;
            return value;
        }

        public void Set(IProperty property, object value)
        {
            CheckProperty(property);
            _values[property.CompiledIndex] = value ?? DBNull.Value;
        }
    }
}
