using System;
using System.Collections.Generic;
using System.Text;

namespace OptKit.ComponentModel
{
    /// <summary>
    /// 标签标记
    /// </summary>
    public class LabelAttribute : Attribute
    {
        /// <summary>
        /// 标签
        /// </summary>
        public string Label { get; set; }

        /// <summary>
        /// 构造实例
        /// </summary>
        public LabelAttribute() { }

        /// <summary>
        /// 构造实例
        /// </summary>
        /// <param name="label">标签</param>
        public LabelAttribute(string label) { Label = label; }
    }
}
