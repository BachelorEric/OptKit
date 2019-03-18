using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace OptKit.XUnit.ComponentModel
{
    class ClassCustomType : ICustomTypeDescriptor
    {
        public AttributeCollection GetAttributes()
        {
            return AttributeCollection.Empty;
        }

        public string GetClassName()
        {
            return "Class2";
        }

        public string GetComponentName()
        {
            return "Class2";
        }

        public TypeConverter GetConverter()
        {
            return null;
        }

        public EventDescriptor GetDefaultEvent()
        {
            return null;
        }

        public PropertyDescriptor GetDefaultProperty()
        {
            return null;
        }

        public object GetEditor(Type editorBaseType)
        {
            return null;
        }

        public EventDescriptorCollection GetEvents()
        {
            return EventDescriptorCollection.Empty;
        }

        public EventDescriptorCollection GetEvents(Attribute[] attributes)
        {
            return EventDescriptorCollection.Empty;
        }

        public PropertyDescriptorCollection GetProperties()
        {
            return GetProperties(null);
        }

        public PropertyDescriptorCollection GetProperties(Attribute[] attributes)
        {
            PropertyDescriptorCollection collection = new PropertyDescriptorCollection(new PropertyDescriptor[] { new Class1PropertyDescriptor("Name", new Attribute[0]) });
            return collection;
        }

        public object GetPropertyOwner(PropertyDescriptor pd)
        {
            return this;
        }

        class Class1PropertyDescriptor : PropertyDescriptor
        {
            public Class1PropertyDescriptor(string name, Attribute[] attributes) : base(name, attributes)
            {
            }

            public override Type ComponentType => throw new NotImplementedException();

            public override bool IsReadOnly => throw new NotImplementedException();

            public override Type PropertyType => throw new NotImplementedException();

            public override bool CanResetValue(object component)
            {
                throw new NotImplementedException();
            }

            public override object GetValue(object component)
            {
                throw new NotImplementedException();
            }

            public override void ResetValue(object component)
            {
                throw new NotImplementedException();
            }

            public override void SetValue(object component, object value)
            {
                throw new NotImplementedException();
            }

            public override bool ShouldSerializeValue(object component)
            {
                throw new NotImplementedException();
            }
        }
    }
}
