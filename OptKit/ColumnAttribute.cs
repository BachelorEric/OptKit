using System;
using System.Collections.Generic;
using System.Text;

namespace OptKit
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class ColumnAttribute : Attribute
    {
    }
}
