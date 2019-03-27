using OptKit.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OptKit.Runtime
{
    /// <summary>
    /// 服务端应用
    /// </summary>
    public class ServerApp : AppBase
    {
        /// <summary>
        /// 启动
        /// </summary>
        public void Startup()
        {
            try
            {
                StartupApplication();
            }
            catch (System.Reflection.ReflectionTypeLoadException exc)
            {
                string message = "服务启动异常：" + exc.LoaderExceptions.Select(p => p.Message).Join("\r\n");
                Logger.Error(message, exc);
                throw new SystemException(message, exc);
            }
            catch (Exception ex)
            {
                Logger.Error("服务启动异常", ex);
                throw;
            }
        }

        /// <summary>
        /// 当外部程序在完全退出时，通过领域应用程序也同时退出。
        /// </summary>
        public void Stop()
        {
            try
            {
                OnExit();
            }
            catch (Exception ex)
            {
                Logger.Error("领域应用程序退出时发生异常", ex);
                throw;
            }
        }
    }
}
