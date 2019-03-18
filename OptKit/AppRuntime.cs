using OptKit.Configuration;
using OptKit.DataPortal;
using OptKit.Logging;
using OptKit.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace OptKit
{
    /// <summary>
    /// 应用运行时工具类
    /// </summary>
    public class AppRuntime
    {
        static DataPortalMode? _dataPortalMode;
        /// <summary>
        /// 数据访问模式
        /// </summary>
        public static DataPortalMode DataPortalMode
        {
            get
            {
                if (_dataPortalMode == null) _dataPortalMode = RT.Config.Get("DataPortalMode", DataPortalMode.Local);
                return _dataPortalMode.Value;
            }
        }
        /// <summary>
        /// 服务容器
        /// </summary>
        public static IServiceContainer Service { get; internal set; } = new ServiceContainer();

        /// <summary>
        /// 日志
        /// </summary>
        public static ILog Logger { get { return LogService.Logger; } }

        /// <summary>
        /// 配置
        /// </summary>
        public static IConfig Config { get; internal set; }

        /// <summary>
        /// 租户ID
        /// </summary>
        public static int TenantId { get; set; }
    }

    public class RT : AppRuntime { }
}
