using OptKit.IO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace OptKit.Configuration
{
    /// <summary>
    /// 配置扩展方法
    /// </summary>
    public static class ConfigExtension
    {
        /// <summary>
        /// 获取指定名称的连接字符串
        /// </summary>
        /// <param name="cfg"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public static ConnectionStringSection GetConnectionString(this IConfig cfg, string name)
        {
            Check.NotNullOrEmpty(name, nameof(name));
            return cfg.GetSection("ConnectionStrings").Get<ConnectionStringSection>(name);
        }

        /// <summary>
        /// 使用Json格式的配置文件
        /// </summary>
        /// <param name="config"></param>
        /// <param name="fileName">文件名，文件位置程序启动目录</param>
        /// <returns></returns>
        public static ConfigManager UserJsonConfig(this ConfigManager config, string fileName)
        {
            Check.NotNullOrEmpty(fileName, nameof(fileName));
            var file = DirectoryName.Create(AppDomain.CurrentDomain.BaseDirectory).CombineFile(fileName);
            RT.Config = new Config(file, JsonConfigSection.Load(file));
            return config;
        }

        /// <summary>
        /// 使用Xml格式的配置文件
        /// </summary>
        /// <param name="config"></param>
        /// <param name="fileName">文件名，文件位置程序启动目录</param>
        /// <returns></returns>
        public static ConfigManager UserXmlConfig(this ConfigManager config, string fileName)
        {
            Check.NotNullOrEmpty(fileName, nameof(fileName));
            var file = DirectoryName.Create(AppDomain.CurrentDomain.BaseDirectory).CombineFile(fileName);
            RT.Config = new Config(file, XmlConfigSection.Load(file));
            return config;
        }

        /// <summary>
        /// 使用指定的配置
        /// </summary>
        /// <param name="config"></param>
        /// <param name="cfg">配置</param>
        /// <returns></returns>
        public static ConfigManager UserConfig(this ConfigManager config, IConfig cfg)
        {
            Check.NotNull(cfg, nameof(cfg));
            RT.Config = cfg;
            return config;
        }
    }
}
