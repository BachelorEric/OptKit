using System.Data;

namespace OptKit.Data.Transaction
{
    /// <summary>
    /// 单连接事务块。
    /// 
    /// 可用于声明一个事务块，在这个事务块中，同一个线程执行的代码，如果是访问同一个数据库，则只会用到同一个数据库连接。
    /// 这样也就不会造成为分布式事务。（分布式事务在一些数据库中并不支持，例如 SQLCE。）
    /// 
    /// 连接与事务的关系，见：<see cref="TransactionScopeConnectionManager"/>。
    /// 事务与线程及上下文的关系，见：<see cref="LocalTransactionBlock"/>。
    /// </summary>
    public class SingleTransactionScope : LocalTransactionBlock
    {
        private IConnectionManager _conMgr;

        /// <summary>
        /// 构造一个事务块
        /// </summary>
        /// <param name="dbSetting">整个数据库的配置名</param>
        public SingleTransactionScope(DbSetting dbSetting) : base(dbSetting, IsolationLevel.Unspecified) { }

        /// <summary>
        /// 构造一个事务块
        /// </summary>
        /// <param name="dbSetting">整个数据库的配置名</param>
        /// <param name="level">事务的孤立级别</param>
        public SingleTransactionScope(DbSetting dbSetting, IsolationLevel level) : base(dbSetting, level) { }

        protected override IDbTransaction BeginTransaction()
        {
            //只要找到一个当前数据库的连接管理对象，直到发生 Dispose 前，
            //这个连接都一直不会被关闭，那么代码块中的数据访问方法都是使用同一个打开的连接。
            //这样就不会升级为分布式事务。
            _conMgr = TransactionScopeConnectionManager.GetManager(DbSetting);
            return _conMgr.Connection.BeginTransaction(IsolationLevel);
        }

        protected override void Cleanup(bool disposing)
        {
            base.Cleanup(disposing);
            if (disposing && _conMgr != null)
                _conMgr.Dispose();
        }
    }
}

