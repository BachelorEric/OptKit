using System;
using System.Collections.Generic;
using System.Text;

namespace OptKit
{
    /// <summary>
    /// 属性声明标记
    /// </summary>
    [AttributeUsage(AttributeTargets.Assembly, AllowMultiple = true)]
    public class PropertyDeclareAttribute : Attribute
    {
        public Type DeclareType { get; set; }

        public PropertyDeclareAttribute(Type declareType)
        {
            DeclareType = declareType;
        }
    }
}
