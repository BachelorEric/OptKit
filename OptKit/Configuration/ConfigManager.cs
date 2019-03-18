using OptKit.IO;
using System;
using System.Collections.Generic;
using System.Text;

namespace OptKit.Configuration
{
    /// <summary>
    /// 配置管理
    /// </summary>
    public class ConfigManager
    {
        /// <summary>
        /// 创建配置管理
        /// </summary>
        /// <returns></returns>
        public static ConfigManager Create()
        {
            return new ConfigManager();
        }

        internal static IConfig CreateDefault()
        {
            var file = DirectoryName.Create(AppDomain.CurrentDomain.BaseDirectory).CombineFile("appsettings.json");
            return new Config(file, JsonConfigSection.Load(file));
        }
    }
}
