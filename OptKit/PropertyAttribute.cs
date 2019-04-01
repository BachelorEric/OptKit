using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace OptKit
{
    /// <summary>
    /// 属性标记
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class PropertyAttribute : Attribute
    {
        /// <summary>
        /// 标签
        /// </summary>
        public string Label { get; set; }
        /// <summary>
        /// 引用实体属性对应的引用ID属性
        /// </summary>
        public string RefIdProperty { get; set; }

        /// <summary>
        /// 引用属性引用类型
        /// </summary>
        public ReferenceType? ReferenceType { get; set; }

        /// <summary>
        /// 视图属性视图路径
        /// </summary>
        public string ViewPath { get; set; }

        /// <summary>
        /// 计算属性表达式
        /// </summary>
        public string Expression { get; set; }

        /// <summary>
        /// 列表属性关联类型
        /// </summary>
        public HasManyType? HasManyType { get; set; }
    }
    /// <summary>
    /// 引用的类型。
    /// </summary>
    public enum ReferenceType
    {
        /// <summary>
        /// 一般的外键引用
        /// </summary>
        Normal,

        /// <summary>
        /// 此引用表示父实体的引用
        /// </summary>
        Parent,
    }
    /// <summary>
    /// 一对多子属性的类型
    /// </summary>
    public enum HasManyType
    {
        /// <summary>
        /// 组合（子对象）
        /// </summary>
        Composition,

        /// <summary>
        /// 聚合（简单引用）
        /// </summary>
        Aggregation
    }
}
