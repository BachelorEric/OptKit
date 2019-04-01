using OptKit.Domain.Metadata;
using System;
using System.Collections.Generic;
using System.Text;

namespace OptKit
{
    /// <summary>
    /// 元数据配置
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class DomainConfig<T> : DomainConfig
    {
        protected internal override Type DomainType { get { return typeof(T); } }
    }
}
