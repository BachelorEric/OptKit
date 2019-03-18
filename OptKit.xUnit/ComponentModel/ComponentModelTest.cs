using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using Xunit;

namespace OptKit.XUnit.ComponentModel
{
    public class ComponentModelTest
    {
        [Fact]
        public void CustomTypeDescriptor()
        {
            var instance = new ClassCustomType();
            var properies = TypeDescriptor.GetProperties(instance);
            Assert.NotEmpty(properies);
        }

        [Fact]
        public void TrackableClass()
        {
            var a = new TrackableClass();
            string propertyName = null;
            object newValue = null;
            a.ValueChanged += (x, y) => { propertyName = y.PropertyName; newValue = y.NewValue; };
            a.Name = "NewName";
            Assert.Equal("Name", propertyName);
            Assert.Equal("NewName", newValue);
        }
    }
}
