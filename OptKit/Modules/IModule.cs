using System;
using System.Collections.Generic;
using System.Text;

namespace OptKit.Modules
{
    /// <summary>
    /// 模块声明
    /// </summary>
    public interface IModule
    {
        /// <summary>
        /// 执行初始化
        /// </summary>
        /// <param name="app"></param>
        void Init(IApp app);

        /// <summary>
        /// 模块初始化前，扫描模块程序集内所有类
        /// </summary>
        /// <param name="type"></param>
        void OnTypeFound(ModuleAssembly module, Type type);
    }
}
