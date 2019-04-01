using System;
using System.Collections.Generic;
using System.Text;

namespace OptKit
{
    /// <summary>
    /// 实体标记
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct, AllowMultiple = false)]
    public class DomainAttribute : Attribute
    {
        public DomainAttribute() { }
        /// <summary>
        /// 显示的属性
        /// </summary>
        public string DisplayMember { get; set; }
        /// <summary>
        /// 查询的属性
        /// </summary>
        public string[] QueryMembers { get; set; }
        /// <summary>
        /// 是否使用通用标准查询
        /// </summary>
        public bool UseCriteriaQuery { get; set; }
        /// <summary>
        /// 查询实体类型
        /// </summary>
        public Type QueryType { get; set; }
        /// <summary>
        /// 实体分类
        /// </summary>
        public DomainCategory Category { get; set; }
        /// <summary>
        /// 是否树实体
        /// </summary>
        public bool IsTree { get; set; }
        /// <summary>
        /// 是否启动假删除
        /// </summary>
        public bool IsPhantom { get; set; }
    }

    public enum DomainCategory
    {
        /// <summary>
        /// 根对象
        /// </summary>
        Root,
        /// <summary>
        /// 孩子对象
        /// </summary>
        Child,
    }
}
