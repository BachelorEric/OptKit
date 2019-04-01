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
        /// <summary>
        /// 执行初始化，重写此方法初始化<see cref="IApp"/>事件
        /// </summary>
        /// <param name="app"></param>
        public virtual void Init(IApp app) { }

        /// <summary>
        /// 模块初始化前，扫描模块程序集内所有类
        /// </summary>
        /// <param name="type"></param>
        public virtual void OnTypeFound(ModuleAssembly module, Type type) { }
    }

    /// <summary>
    /// 界面模块
    /// </summary>
    public abstract class UIModule : IModule
    {
        /// <summary>
        /// 执行初始化，重写此方法初始化<see cref="IApp"/>事件
        /// </summary>
        /// <param name="app"></param>
        public virtual void Init(IApp app) { }

        /// <summary>
        /// 模块初始化前，扫描模块程序集内所有类
        /// </summary>
        /// <param name="type"></param>
        public virtual void OnTypeFound(ModuleAssembly module, Type type) { }
    }
}
