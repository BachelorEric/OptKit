using OptKit.DataPortal;
using System;
using System.Collections.Generic;
using System.Text;

namespace OptKit.Runtime
{
    /// <summary>
    /// 运行时环境
    /// </summary>
    public class RuntimeEnvironment
    {
        public RuntimeEnvironment()
        {
            RootDirectory = AppDomain.CurrentDomain.BaseDirectory;
            DllRootDirectory = RootDirectory;
            IsDebuggingEnabled = RT.Config.Get("IsDebuggingEnabled", false);
        }

        /// <summary>
        /// 整个应用程序的根目录
        /// </summary>
        public string RootDirectory { get; set; }

        /// <summary>
        /// Dll 存在的目录路径
        /// （Web 项目的路径是 RootDirectory+"/Bin"）
        /// </summary>
        public string DllRootDirectory { get; set; }

        /// <summary>
        /// 在程序启动时，设置本属性以指示当前程序是否处于调试状态。
        /// </summary>
        public bool IsDebuggingEnabled { get; set; }


        DataPortalMode? _dataPortalMode;
        /// <summary>
        /// 数据访问模式
        /// </summary>
        public DataPortalMode DataPortalMode
        {
            get
            {
                if (_dataPortalMode == null) _dataPortalMode = RT.Config.Get("DataPortalMode", DataPortalMode.Local);
                return _dataPortalMode.Value;
            }
        }
    }
}

