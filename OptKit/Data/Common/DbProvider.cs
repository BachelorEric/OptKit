using OptKit.Domain.Mapping;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Text;

namespace OptKit.Data.Common
{
    /// <summary>
    /// 数据库提供者，默认实现System.Data.SqlClient、System.Data.OracleClient、Oracle.DataAccess.Client、Oracle.ManagedDataAccess.Client.
    /// 其它数据库则需要继承<see cref="DbProvider"/>，并通过<see cref="DbProvider.RegisterDbProvider(string, DbProvider)"/>注册.
    /// </summary>
    public abstract class DbProvider
    {
        public const string SqlClient = "System.Data.SqlClient";
        public const string Oracle = "System.Data.OracleClient";
        public const string ODAC = "Oracle.DataAccess.Client";
        public const string ODP = "Oracle.ManagedDataAccess.Client";

        public const string LocalServer = "LocalSqlServer";

        static DbProviderFactory _sql, _oracle, _odac, _opd;
        static Dictionary<string, DbProviderFactory> _dbProviderFactory = new Dictionary<string, DbProviderFactory>();
        static Dictionary<string, DbProvider> _providers = new Dictionary<string, DbProvider>();
        static ISqlDialect _sqlDialect, _oracleDialect;

        /// <summary>
        /// 注册数据库提供者
        /// </summary>
        /// <param name="providerName"></param>
        /// <param name="provider"></param>
        public static void RegisterDbProvider(string providerName, DbProvider provider)
        {
            _providers.Add(providerName, provider);
        }

        /// <summary>
        /// 以快速键值对照来获取 DbProviderFactory。
        /// </summary>
        /// <param name="provider"></param>
        /// <returns></returns>
        public static DbProviderFactory GetFactory(string provider)
        {
            switch (provider)
            {
                case SqlClient:
                    return (_sql ?? (_sql = DbProviderFactories.GetFactory(provider)));
                case Oracle:
                    return (_oracle ?? (_oracle = DbProviderFactories.GetFactory(provider)));
                case ODAC:
                    return (_odac ?? (_odac = DbProviderFactories.GetFactory(provider)));
                case ODP:
                    return (_opd ?? (_opd = DbProviderFactories.GetFactory(provider)));
            }
            DbProviderFactory factory;
            if (!_dbProviderFactory.TryGetValue(provider, out factory))
            {
                lock (_dbProviderFactory)
                {
                    if (!_dbProviderFactory.TryGetValue(provider, out factory))
                    {
                        factory = DbProviderFactories.GetFactory(provider);
                        _dbProviderFactory.Add(provider, factory);
                    }
                }
            }
            return factory;
        }

        /// <summary>
        /// 获取数据库提供者，除SqlServer和Oracle外，其它数据库需要通过<see cref="DbProvider.RegisterDbProvider(string, DbProvider)"/>注册
        /// </summary>
        /// <param name="providerName"></param>
        /// <returns></returns>
        public static DbProvider GetProvider(string providerName)
        {
            DbProvider provider;
            if (_providers.TryGetValue(providerName, out provider))
                return provider;
            throw new NotSupportedException("不支持:" + providerName);
        }

        /// <summary>
        /// 获取数据库方言
        /// </summary>
        /// <param name="providerName"></param>
        /// <returns></returns>
        public static ISqlDialect GetDialect(string providerName)
        {
            switch (providerName)
            {
                case SqlClient:
                    return (_sqlDialect ?? (_sqlDialect = new SqlClient.SqlServerDialect()));
                case Oracle:
                case ODP:
                case ODAC:
                    return (_oracleDialect ?? (_oracleDialect = new Oracle.OracleDialect()));
            }
            DbProvider provider = GetProvider(providerName);
            return provider.CreateDialect(providerName);
        }

        ///// <summary>
        ///// 创建<see cref="DbTable"/>
        ///// </summary>
        ///// <param name="setting"></param>
        ///// <param name="info"></param>
        ///// <returns></returns>
        //public static RdbTable CreateTable(DbSetting setting, ITableInfo info)
        //{
        //    switch (setting.ProviderName)
        //    {
        //        case SqlClient:
        //            return new SqlServerTable(info);
        //        case Oracle:
        //        case ODAC:
        //        case ODP:
        //            return new OracleTable(info);
        //    }
        //    DbProvider provider = GetProvider(setting.ProviderName);
        //    return provider.CreateDbTable(setting, info);
        //}

        protected abstract ISqlDialect CreateDialect(string provider);

        //protected abstract RdbTable CreateDbTable(DbSetting setting, ITableInfo info);
    }
}
