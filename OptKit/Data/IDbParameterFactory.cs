using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace OptKit.Data
{
    /// <summary>
    /// 数据库命令参数工厂，创建关系数据库命令的参数
    /// </summary>
    public interface IDbParameterFactory
    {
        /// <summary>
        /// 创建一个数据库参数
        /// </summary>
        /// <returns>sql命令的参数</returns>
        IDbDataParameter CreateParameter();

        /// <summary>
        /// 创建一个数据库参数
        /// </summary>
        /// <param name="name">参数名称</param>
        /// <returns>sql命令的参数</returns>
        IDbDataParameter CreateParameter(string name);

        /// <summary>
        /// 创建一个数据库参数
        /// </summary>
        /// <param name="name">参数名称</param>
        /// <param name="value">参数值</param>
        /// <returns>sql命令的参数</returns>
        IDbDataParameter CreateParameter(string name, object value);

        /// <summary>
        /// 创建一个数据库参数
        /// </summary>
        /// <param name="name">参数名称</param>
        /// <param name="value">参数值</param>
        /// <param name="type">参数类型</param>
        /// <returns>sql命令的参数</returns>
        IDbDataParameter CreateParameter(string name, object value, DbType type);

        /// <summary>
        /// 创建一个数据库参数
        /// </summary>
        /// <param name="name">参数名称</param>
        /// <param name="value">参数值</param>
        /// <param name="direction">参数方向</param>
        /// <returns>sql命令的参数</returns>
        IDbDataParameter CreateParameter(string name, object value, ParameterDirection direction);

        /// <summary>
        /// 创建一个数据库参数
        /// </summary>
        /// <param name="name">参数名称</param>
        /// <param name="value">参数值</param>
        /// <param name="type">参数类型</param>
        /// <param name="size">参数大小</param>
        /// <returns>sql命令的参数</returns>
        IDbDataParameter CreateParameter(string name, object value, DbType type, int size);

        /// <summary>
        /// 创建一个数据库参数
        /// </summary>
        /// <param name="name">参数名称</param>
        /// <param name="value">参数值</param>
        /// <param name="type">参数类型</param>
        /// <param name="direction">参数方向</param>
        /// <returns>sql命令的参数</returns>
        IDbDataParameter CreateParameter(string name, object value, DbType type, ParameterDirection direction);

        /// <summary>
        /// 创建一个数据库参数
        /// </summary>
        /// <param name="name">参数名称</param>
        /// <param name="value">参数值</param>
        /// <param name="type">参数类型</param>
        /// <param name="size">参数大小</param>
        /// <param name="direction">参数方向</param>
        /// <returns>sql命令的参数</returns>
        IDbDataParameter CreateParameter(string name, object value, DbType type, int size, ParameterDirection direction);
    }
}
