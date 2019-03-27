using OptKit.Configuration;
using OptKit.DataPortal;
using OptKit.Logging;
using OptKit.Modules;
using OptKit.Runtime;
using OptKit.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace OptKit
{
    /// <summary>
    /// 应用运行时工具类, AppRuntime缩写
    /// </summary>
    public partial class RT
    {
        /// <summary>
        /// 应用
        /// </summary>
        public static IApp App { get; internal set; }

        static RuntimeEnvironment environment;
        /// <summary>
        /// 运行时环境
        /// </summary>
        public static RuntimeEnvironment Environment { get { return environment ?? (environment = new RuntimeEnvironment()); } }

        static IServiceContainer service;
        /// <summary>
        /// 服务容器
        /// </summary>
        public static IServiceContainer Service { get { return service ?? (service = new ServiceContainer()); } internal set { service = value; } }

        static ILog logger;
        /// <summary>
        /// 日志
        /// </summary>
        public static ILog Logger { get { return logger ?? (logger = LogService.GetLogger("runtime_logger")); } internal set { logger = value; } }

        /// <summary>
        /// 配置
        /// </summary>
        public static IConfig Config { get; internal set; }

        /// <summary>
        /// 租户ID
        /// </summary>
        public static int TenantId { get; set; }
    }
}
