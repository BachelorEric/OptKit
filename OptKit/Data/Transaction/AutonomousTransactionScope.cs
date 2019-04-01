using OptKit.Context;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OptKit.Data.Transaction
{
    /// <summary>
    /// 自治事务，允许离开调用的事务上下文，执行一个独立的事务
    /// </summary>
    public class AutonomousTransactionScope : SingleTransactionScope
    {
        /// <summary>
        /// 构造一个事务块
        /// </summary>
        /// <param name="dbSetting">整个数据库的配置名</param>
        public AutonomousTransactionScope(DbSetting dbSetting) : base(dbSetting, IsolationLevel.Unspecified)
        {
        }

        /// <summary>
        /// 构造一个事务块
        /// </summary>
        /// <param name="dbSetting">整个数据库的配置名</param>
        /// <param name="level">事务的孤立级别</param>
        public AutonomousTransactionScope(DbSetting dbSetting, IsolationLevel level) : base(dbSetting, level)
        {
        }

        protected override void EnterScope(string contextKey)
        {
            _contextKey = contextKey;
            _lastScope = GetWholeScope(contextKey, Context);
            Context.Remove(contextKey);
            if (Context.ContainsKey(GetScopeIdName))
            {
                _id = Context[GetScopeIdName];
                Context.Remove(GetScopeIdName);
            }
            base.EnterScope(contextKey);
        }

        string _contextKey;
        ContextScope _lastScope;
        object _id;

        protected override void Cleanup(bool disposing)
        {
            base.Cleanup(disposing);
            if (disposing)
            {
                if (_id != null)
                    Context[GetScopeIdName] = _id;
                if (_lastScope != null)
                    Context[_contextKey] = _lastScope;
            }
        }
    }
}

