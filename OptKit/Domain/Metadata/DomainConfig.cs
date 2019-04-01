using System;
using System.Collections.Generic;
using System.Text;

namespace OptKit.Domain.Metadata
{
    public abstract class DomainConfig
    {
        /// <summary>
        /// 所处模块的启动级别。
        /// </summary>
        internal int SetupIndex { get; set; }

        /// <summary>
        /// 继承层次
        /// </summary>
        internal int InheritanceCount { get; set; }

        /// <summary>
        /// 本实体配置对应的实体类
        /// </summary>
        protected internal abstract Type DomainType { get; }

        /// <summary>
        /// 子类重写此方法，并完成对 Meta 属性的配置。
        /// 
        /// 注意：
        /// * 为了给当前类的子类也运行同样的配置，这个方法可能会被调用多次。
        /// </summary>
        protected internal virtual void ConfigMeta(DomainMeta meta) { }
    }
}
