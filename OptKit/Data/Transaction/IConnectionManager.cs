using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OptKit.Data.Transaction
{
    /// <summary>
    /// 一个连接的管理容器
    /// </summary>
    internal interface IConnectionManager : IDisposable
    {
        /// <summary>
        /// 对应的连接对象。
        /// </summary>
        IDbConnection Connection { get; }

        /// <summary>
        /// 对应的数据库配置信息
        /// </summary>
        DbSetting DbSetting { get; }
    }
}
