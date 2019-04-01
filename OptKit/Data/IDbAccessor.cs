using OptKit.Data.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace OptKit.Data
{
    /// <summary>
    /// 数据访问接口，基于ADO.NET的数据访问接口
    /// </summary>
    public interface IDbAccesser : IDisposable
    {
        ISqlDialect SqlDialect { get; }

        /// <summary>
        /// 关系数据库链接
        /// </summary>
        IDbConnection Connection { get; }

        /// <summary>
        /// 执行sql返回受影响行数
        /// </summary>
        /// <param name="sql">指定的sql</param>
        /// <param name="type">指定<see cref="System.Data.IDbCommand.CommandText"/>如何被解析</param>
        /// <param name="parameters">sql所包含的参数</param>
        /// <returns>受影响行数</returns>
        int ExecuteNonQuery(string sql, CommandType type, params IDbDataParameter[] parameters);

        /// <summary>
        /// 执行sql返回结果集第一行第一列，忽略其它值
        /// </summary>
        /// <param name="sql">指定的sql</param>
        /// <param name="type">指定<see cref="System.Data.IDbCommand.CommandText"/>如何被解析</param>
        /// <param name="parameters">sql所包含的参数</param>
        /// <returns><see cref="DBNull"/>或者对象</returns>
        object ExecuteScalar(string sql, CommandType type, params IDbDataParameter[] parameters);

        /// <summary>
        /// 从关系数据库中查询数据
        /// </summary>
        /// <param name="sql">指定的sql</param>
        /// <param name="type">指定<see cref="System.Data.IDbCommand.CommandText"/>如何被解析</param>
        /// <param name="closeConnection">数据读取器关闭的时候是否关闭链接</param>
        /// <param name="parameters">sql所包含的参数</param>
        /// <returns>数据安全读取器</returns>
        SafeDataReader ExecuteReader(string sql, CommandType type, bool closeConnection, params IDbDataParameter[] parameters);

        /// <summary>
        /// 从关系数据库中查询数据并返回<see cref="DataTable"/>
        /// </summary>
        /// <param name="sql">指定的sql</param>
        /// <param name="type">指定<see cref="System.Data.IDbCommand.CommandText"/>如何被解析</param>
        /// <param name="parameters">sql所包含的参数</param>
        /// <returns>数据表</returns>
        DataTable ExecuteDataTable(string sql, CommandType type, params IDbDataParameter[] parameters);

        /// <summary>
        /// 创建数据库命令
        /// </summary>
        /// <param name="sql">指定的sql</param>
        /// <param name="type">指定<see cref="System.Data.IDbCommand.CommandText"/>如何被解析</param>
        /// <param name="parameters">sql所包含的参数</param>
        /// <returns></returns>
        IDbCommand CreateCommand(string sql, CommandType type, params IDbDataParameter[] parameters);

        /// <summary>
        /// 获取数据库命令参数工厂
        /// </summary>
        IDbParameterFactory ParameterFactory { get; }
    }
}
