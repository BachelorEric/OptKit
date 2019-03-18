using OptKit.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace OptKit.Primitives.Domain
{
    class PropertyContainerFactoryImpl : PropertyContainerFactory
    {
        public override IPropertyContainer Get(Type type)
        {
            var container = new PropertyContainer();
            return container;
        }
    }
}
