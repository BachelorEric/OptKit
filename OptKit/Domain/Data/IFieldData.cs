using System;
using System.Collections.Generic;
using System.Text;

namespace OptKit.Domain.Data
{
    public interface IFieldData
    {
        object Get(IProperty property);
        bool Exists(IProperty property);
        void Set(IProperty property, object value);
    }
}
