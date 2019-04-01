using OptKit.Configuration;
using OptKit.Data.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Text.RegularExpressions;

namespace OptKit.Data
{
    /// <summary>
    /// 数据库连接结构/方案
    /// </summary>
    public class DbSetting
    {
        string _database;

        public DbSetting(string connectionString, string providerName)
        {
            ConnectionString = connectionString;
            ProviderName = providerName;
        }

        /// <summary>
        /// 子类使用
        /// </summary>
        internal DbSetting() { }

        /// <summary>
        /// 连接字符串
        /// </summary>
        public string ConnectionString { get; internal set; }

        /// <summary>
        /// 连接的提供器名称
        /// </summary>
        public string ProviderName { get; internal set; }

        /// <summary>
        /// 对应的数据库名称
        /// </summary>
        public string Database
        {
            get { return (_database ?? (_database = ParseDbName())); }
        }

        string ParseDbName()
        {
            var con = CreateConnection();
            var database = con.Database;

            //System.Data.OracleClient 解析不出这个值，需要特殊处理。
            if (database.IsNullOrWhiteSpace())
            {
                //Oracle 中，把用户名（Schema）认为数据库名。
                var match = Regex.Match(ConnectionString, @"User Id=\s*(?<dbName>\w+)\s*");
                if (!match.Success)
                {
                    throw new NotSupportedException("无法解析出此数据库连接字符串中的数据库名：" + ConnectionString);
                }
                database = match.Groups["dbName"].Value;
            }

            return database;
        }

        /// <summary>
        /// 使用当前的结构来创建一个连接。
        /// </summary>
        /// <returns></returns>
        public IDbConnection CreateConnection()
        {
            var factory = DbProvider.GetFactory(ProviderName);

            var connection = factory.CreateConnection();
            connection.ConnectionString = ConnectionString;

            return connection;
        }

        /// <summary>
        /// 配置名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 查找或者根据约定创建连接字符串
        /// </summary>
        /// <param name="dbSettingName"></param>
        /// <returns></returns>
        public static DbSetting FindOrCreate(string dbSettingName)
        {
            Check.NotNull(dbSettingName, nameof(dbSettingName));

            DbSetting setting = null;

            if (!_generatedSettings.TryGetValue(dbSettingName, out setting))
            {
                lock (_generatedSettings)
                {
                    if (!_generatedSettings.TryGetValue(dbSettingName, out setting))
                    {
                        var config = RT.Config.GetConnectionString(dbSettingName);
                        if (config == null)
                            throw new DataException("找不到名为[{0}]的ConnectionString".FormatArgs(dbSettingName));
                        setting = new DbSetting
                        {
                            Name = dbSettingName,
                            ConnectionString = config.ConnectionString,
                            ProviderName = config.ProviderName,
                        };

                        _generatedSettings.Add(dbSettingName, setting);
                    }
                }
            }

            return setting;
        }

        /// <summary>
        /// 添加一个数据库连接配置。
        /// </summary>
        /// <param name="name"></param>
        /// <param name="connectionString"></param>
        /// <param name="providerName"></param>
        public static DbSetting SetSetting(string name, string connectionString, string providerName)
        {
            Check.NotNullOrEmpty(name, nameof(name));
            Check.NotNullOrEmpty(connectionString, nameof(connectionString));
            Check.NotNullOrEmpty(providerName, nameof(providerName));

            var setting = new DbSetting
            {
                Name = name,
                ConnectionString = connectionString,
                ProviderName = providerName
            };

            lock (_generatedSettings)
            {
                _generatedSettings[name] = setting;
            }

            return setting;
        }

        /// <summary>
        /// 获取当前已经被生成的 DbSetting。
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<DbSetting> GetGeneratedSettings()
        {
            return _generatedSettings.Values;
        }

        static Dictionary<string, DbSetting> _generatedSettings = new Dictionary<string, DbSetting>();
    }
}
