using System;
using System.Collections.Generic;
using System.Text;

namespace OptKit.Modules
{
    /// <summary>
    /// 服务模块
    /// </summary>
    public abstract class ServiceModule : IModule
    {
        public abstract void Init(IApp app);
    }

    /// <summary>
    /// 界面模块
    /// </summary>
    public abstract class UIModule : IModule
    {
        public abstract void Init(IApp app);
    }
}
