using OptKit.Data.Transaction;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace OptKit.Data
{
    /// <summary>
    /// 数据库事件参数
    /// </summary>
    public class DbEventArgs : EventArgs
    {
        /// <summary>
        /// 数据库命令
        /// </summary>
        public IDbCommand DbCommand { get; set; }
        /// <summary>
        /// 事务范围ID
        /// </summary>
        public string ScopeId { get; set; }
        /// <summary>
        /// 数据库命令执行失败的异常
        /// </summary>
        public Exception Exception { get; set; }
        /// <summary>
        /// 构造数据库事件参数
        /// </summary>
        public DbEventArgs()
        {
            ScopeId = LocalTransactionBlock.GetScopeId();
        }
        /// <summary>
        /// 构造数据库事件参数
        /// </summary>
        /// <param name="command">数据库命令</param>
        public DbEventArgs(IDbCommand command)
        {
            ScopeId = LocalTransactionBlock.GetScopeId();
            DbCommand = command;
        }
        /// <summary>
        /// 构造数据库事件参数
        /// </summary>
        /// <param name="command">数据库命令</param>
        /// <param name="exc">数据库命令执行失败的异常</param>
        public DbEventArgs(IDbCommand command, Exception exc)
        {
            ScopeId = LocalTransactionBlock.GetScopeId();
            DbCommand = command;
            Exception = exc;
        }
    }
}
