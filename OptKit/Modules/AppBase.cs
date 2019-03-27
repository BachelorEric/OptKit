using OptKit.Domain;
using OptKit.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace OptKit.Modules
{
    /// <summary>
    /// 应用基类
    /// </summary>
    public abstract class AppBase : IApp
    {
        public event EventHandler ModulesIntialized;
        public event EventHandler ServiceIntializing;
        public event EventHandler StartupCompleted;
        public event EventHandler Exit;

        /// <summary>
        /// Gets the logger assosiated with this object.
        /// </summary>
        public ILog Logger { get; } = LogService.GetLogger("app_logger");

        protected void StartupApplication()
        {
            Logger.Info("应用启动");
            RT.App = this;
            OnIntializing();
            SetupEnvironment();
            //注册扩展属性
            DomainManager.RegisterExtensionProperties();
            InitModules();
            OnModulesIntialized();
            Logger.Info("模块初始化");
            OnServiceIntializing();
            OnStartupCompleted();
            Logger.Info("启动成功");
        }

        void InitModules()
        {
            var modules = RT.GetModules();
            //先执行服务模块，再执行界面模块
            foreach (var module in modules)
            {
                if (module.ModuleType.IsSubclassOf(typeof(ServiceModule)))
                {
                    var m = Activator.CreateInstance(module.ModuleType) as IModule;
                    m.Init(this);
                }
            }
            foreach (var module in modules)
            {
                if (module.ModuleType.IsSubclassOf(typeof(UIModule)))
                {
                    var m = Activator.CreateInstance(module.ModuleType) as IModule;
                    m.Init(this);
                }
            }
        }

        /// <summary>
        /// 模块初始化完成事件
        /// </summary>
        protected virtual void OnModulesIntialized()
        {
            ModulesIntialized?.Invoke(this, EventArgs.Empty);
        }

        /// <summary>
        /// 服务初始化事件。
        /// </summary>
        protected virtual void OnServiceIntializing()
        {
            ServiceIntializing?.Invoke(this, EventArgs.Empty);
        }

        /// <summary>
        /// 启动完成事件。
        /// </summary>
        protected virtual void OnStartupCompleted()
        {
            StartupCompleted?.Invoke(this, EventArgs.Empty);
        }

        /// <summary>
        /// 应用退出事件。
        /// </summary>
        protected virtual void OnExit()
        {
            Exit?.Invoke(this, EventArgs.Empty);
        }

        /// <summary>
        /// 开始启动
        /// </summary>
        protected virtual void OnIntializing() { }

        /// <summary>
        /// 设置运行环境
        /// </summary>
        protected virtual void SetupEnvironment() { }
    }
}
