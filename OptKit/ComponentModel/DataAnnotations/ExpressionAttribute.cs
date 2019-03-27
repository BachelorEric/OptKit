using System;
using System.Collections.Generic;
using System.Text;

namespace OptKit.ComponentModel.DataAnnotations
{
    /// <summary>
    /// 计算表达式
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class ExpressionAttribute : Attribute
    {
        public string Expression { get; set; }

        public ExpressionAttribute() { }

        public ExpressionAttribute(string expr) { Expression = expr; }
    }
}
