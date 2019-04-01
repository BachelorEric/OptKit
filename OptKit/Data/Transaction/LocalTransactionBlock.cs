using OptKit.Context;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime;
using System.Text;
using System.Threading.Tasks;

namespace OptKit.Data.Transaction
{
    public abstract class LocalTransactionBlock : ContextScope
    {

        /// <summary>
        /// 是否已经提交完成。
        /// </summary>
        bool _rollback = true;

        IDbTransaction _transaction;
        /// <summary>
        /// 是否需要把整个事务回滚。
        /// </summary>
        bool _wholeRollback;

        /// <summary>
        /// 所使用的存储位置
        /// </summary>
        static IDictionary<string, object> ContextItems
        {
            get { return ThreadStaticAppContextProvider.Items; }
        }

        public static string GetScopeId()
        {
            object id = null;
            ContextItems.TryGetValue(GetScopeIdName, out id);
            return id?.ToString();
        }

        /// <summary>
        /// 构造一个本地事务代码块
        /// </summary>
        /// <param name="dbSetting">数据库配置</param>
        /// <param name="level">
        /// 此级别只在最外层的代码块中有效。
        /// </param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214")]
        public LocalTransactionBlock(DbSetting dbSetting, IsolationLevel level)
            : base(ContextItems)
        {
            DbSetting = dbSetting;
            IsolationLevel = level;
            var name = LocalContextName(DbSetting.Database);
            EnterScope(name);
        }

        /// <summary>
        /// 对应的数据库配置
        /// </summary>
        public DbSetting DbSetting { get; }

        /// <summary>
        /// 对应的事务孤立级别
        /// </summary>
        public IsolationLevel IsolationLevel { get; }

        /// <summary>
        /// 当前范围块正在使用的数据库事务。
        /// </summary>
        public IDbTransaction WholeTransaction
        {
            get
            {
                var ws = WholeScope as LocalTransactionBlock;
                return ws._transaction;
            }
        }

        /// <summary>
        /// 提交本事务。
        /// </summary>
        public void Complete()
        {
            _rollback = false;
        }

        /// <summary>
        /// 子类实现此方法进入指定库的事务。
        /// 
        /// 注意，该方法只会在最外层的 using 块中被调用一次。
        /// 返回的事务，由基类负责它的 Commit、Rollback 和 Dispose，子类不需要管理。
        /// </summary>
        /// <returns></returns>
        protected abstract IDbTransaction BeginTransaction();

        protected override sealed void EnterWholeScope()
        {
            _transaction = BeginTransaction();
            ContextItems.Add(GetScopeIdName, Guid.NewGuid().ToString("N"));
        }

        protected override void Cleanup(bool disposing)
        {
            if (disposing)
            {
                //如果本事务没有被提交，则整个事务也需要回滚。
                var ws = WholeScope as LocalTransactionBlock;
                ws._wholeRollback |= _rollback;
            }
            base.Cleanup(disposing);
        }

        protected override sealed void ExitWholeScope()
        {
            if (_wholeRollback)
            {
                _transaction.Rollback();
                _wholeRollback = false;
                DbAccesserFactory.OnTransactionRollback(this);
            }
            else
            {
                if (_transaction.Connection != null)
                    _transaction.Commit();
            }
            var name = LocalContextName(DbSetting.Database);
            ContextItems.Remove(name);
            ContextItems.Remove(GetScopeIdName);

            if (_transaction.Connection != null)
                _transaction.Connection.Close();
            //不论是正常的提交，还是已经被回滚，最外层的事务块都需要把事务进行释放。
            DisposeTransaction(_transaction);
        }

        /// <summary>
        /// 子类实现此方法释放指定的事务。
        /// </summary>
        /// <returns></returns>
        protected virtual void DisposeTransaction(IDbTransaction tran)
        {
            tran.Dispose();
        }

        #region 静态接口

        internal static IDbTransaction GetCurrentTransaction(string database)
        {
            var block = GetWholeScope(database);
            if (block != null) return block._transaction;
            return null;
        }

        internal static LocalTransactionBlock GetWholeScope(string database)
        {
            var name = LocalContextName(database);
            var ws = GetWholeScope(name, ContextItems) as LocalTransactionBlock;
            return ws;
        }

        [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")]
        internal static string LocalContextName(string databse)
        {
            return "LocalTransactionBlock:" + databse;
        }

        internal static string GetScopeIdName
        {
            get { return "LocalTransactionBlock:ID"; }
        }

        #endregion
    }
}
