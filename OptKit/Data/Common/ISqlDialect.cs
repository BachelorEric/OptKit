using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace OptKit.Data.Common
{
    public interface ISqlDialect
    {
        /// <summary>
        /// 把可用于String.Format格式的字符串转换为特定数据库格式的字符串
        /// </summary>
        /// <param name="commonSql">可用于String.Format格式的字符串</param>
        /// <returns>可用于特定数据库的sql语句</returns>
        string ToSpecialDbSql(string commonSql);

        /// <summary>
        /// 返回用于
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        string GetParameterName(int number);

        /// <summary>
        /// 获取存储过程返回参数名称.
        /// </summary>
        string ProcudureReturnParameterName { get; }

        /// <summary>
        /// 处理参数.
        /// </summary>
        /// <param name="p">命令参数.</param>
        void PrepareParameter(IDbDataParameter p);

        /// <summary>
        /// 处理参数值
        /// </summary>
        /// <param name="value">值</param>
        /// <returns></returns>
        object PrepareValue(object value);

        /// <summary>
        /// 处理命令
        /// </summary>
        /// <param name="command">命令</param>
        void PrepareCommand(IDbCommand command);

        /// <summary>
        /// 处理标识符
        /// </summary>
        /// <param name="identifier">标识符</param>
        /// <returns></returns>
        string PrepareIdentifier(string identifier);

        string LimitIdentifier(string identifier);

        /// <summary>
        /// 数据库时间值的SQL，例如Oracle:"SYSDATE"
        /// </summary>
        /// <returns></returns>
        string DbTimeValueSql();

        /// <summary>
        /// Select数据库时间值的SQL，例如Oracle:"SELECT SYSDATE FROM DUAL"
        /// </summary>
        /// <returns></returns>
        string SelectDbTimeSql();

        /// <summary>
        /// 获取序列下一个值(Oracle,SqlServer2012)，例如Oracle:"SELECT SEQ_XXX.NEXTVAL FROM DUAL"
        /// </summary>
        /// <param name="tableName">表名.</param>
        /// <param name="columnName">字段名.</param>
        /// <returns></returns>
        string SelectSeqNextValueSql(string tableName, string columnName);

        /// <summary>
        /// Seqs the next value SQL.例如Oracle:"SEQ_XXX.NEXTVAL"
        /// </summary>
        /// <param name="tableName">Name of the table.</param>
        /// <param name="columnName">Name of the column.</param>
        /// <returns></returns>
        string SeqNextValueSql(string tableName, string columnName);
    }
}
