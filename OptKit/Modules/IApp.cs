using System;
using System.Collections.Generic;
using System.Text;

namespace OptKit.Modules
{
    /// <summary>
    /// 应用声明
    /// </summary>
    public interface IApp
    {
        /// <summary>
        /// 所有模型元数据初始化完毕，包括模型元数据之间的关系。
        /// </summary>
        event EventHandler ModulesIntialized;

        /// <summary>
        /// 服务初始化
        /// </summary>
        event EventHandler ServiceIntializing;

        /// <summary>
        /// 启动完毕
        /// </summary>
        event EventHandler StartupCompleted;

        /// <summary>
        /// 应用程序完全退出
        /// </summary>
        event EventHandler Exit;
    }
}
