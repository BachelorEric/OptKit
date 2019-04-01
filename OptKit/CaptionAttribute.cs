using System;
using System.Collections.Generic;
using System.Text;

namespace OptKit
{
    /// <summary>
    /// 标题标记
    /// </summary>
    public class CaptionAttribute : Attribute
    {
        /// <summary>
        /// 标题
        /// </summary>
        public string Caption { get; set; }

        /// <summary>
        /// 构造实例
        /// </summary>
        public CaptionAttribute() { }

        /// <summary>
        /// 构造实例
        /// </summary>
        /// <param name="caption">标签</param>
        public CaptionAttribute(string caption) { Caption = caption; }
    }
}
