using System;
using System.Collections.Generic;
using System.Text;

namespace OptKit.Context
{
    /// <summary>
    /// 应用上下文提供者
    /// </summary>
    public interface IAppContextProvider
    {
        /// <summary>
        /// 获取或设置当前的上下文数据容器。
        /// </summary>
        IDictionary<string, object> Context { get; set; }
    }
}
