using OptKit.ComponentModel;
using OptKit.Data.Common;
using OptKit.Data.Transaction;
using OptKit.Diagnostics;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Text;

namespace OptKit.Data
{
    /// <summary>
    /// 关系数据库访问器
    /// </summary>
    public class DbAccesser : IDisposable, IDbAccesser, IDbParameterFactory
    {
        protected void OnError(IDbCommand command, Exception exc)
        {
            DbAccesserFactory.OnError(this, command, exc);
        }

        protected void OnDbCommandPrepared(IDbCommand command)
        {
            DbAccesserFactory.OnDbCommandPrepared(this, command);
        }

        #region fields

        /// <summary>
        /// Was the connection opened by my self.
        /// </summary>
        private bool _openConnectionBySelf;

        /// <summary>
        /// Is this connection created by my self;
        /// </summary>
        private bool _connectionCreatedBySelf;

        private DbSetting _connectionSchema;

        /// <summary>
        /// inner db connection
        /// </summary>
        private IDbConnection _connection;

        /// <summary>
        /// abstract db provider factory
        /// </summary>
        private DbProviderFactory _factory;

        /// <summary>
        /// used to format sql and its corresponding parameters.
        /// </summary>
        private ISqlDialect _sqlDialect;

        //private IDbTransaction _transaction; 

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor
        /// 
        /// this accessor uses <see cref="DbSetting"/> class to find its connection string, and creates connection by itself.
        /// </summary>
        /// <param name="connectionStringSettingName">the setting name in configuration file.</param>
        public DbAccesser(string connectionStringSettingName)
        {
            var setting = DbSetting.FindOrCreate(connectionStringSettingName);
            Init(setting);
        }

        /// <summary>
        /// Constructor
        /// 
        /// this accessor creates the db connection by itself.
        /// </summary>
        /// <param name="connectionString">Connection string</param>
        /// <param name="connectionProvider">
        /// The provider.
        /// eg.
        /// "System.Data.SqlClient"
        /// </param>
        public DbAccesser(string connectionString, string connectionProvider)
        {
            if (string.IsNullOrEmpty(connectionString))
            {
                throw new ArgumentNullException("connectionString");
            }
            if (string.IsNullOrEmpty(connectionProvider))
            {
                throw new ArgumentNullException("connectionProvider");
            }

            Init(new DbSetting(connectionString, connectionProvider));
        }

        /// <summary>
        /// Constructor
        /// 
        /// this accessor uses schema to find its connection string, and creates connection by itself.
        /// </summary>
        /// <param name="schema">the connection schema.</param>
        public DbAccesser(DbSetting schema)
        {
            Init(schema);
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="schema">the connection schema.</param>
        /// <param name="dbConnection">use a exsiting connection, rather than to create a new one.</param>
        public DbAccesser(DbSetting schema, IDbConnection dbConnection)
        {
            Init(schema, dbConnection);
        }

        private void Init(DbSetting schema, IDbConnection connection = null)
        {
            _connectionSchema = schema;

            _factory = DbProvider.GetFactory(schema.ProviderName);
            _sqlDialect = DbProvider.GetDialect(schema.ProviderName);
            if (connection == null)
            {
                _connection = _factory.CreateConnection();
                _connection.ConnectionString = schema.ConnectionString;
                _connectionCreatedBySelf = true;
            }
            else
            {
                _connection = connection;
            }
        }

        #endregion

        #region Properties
        public ISqlDialect SqlDialect
        {
            get { return _sqlDialect; }
        }

        /// <summary>
        /// Gets the connection schema of current database.
        /// </summary>
        public DbSetting ConnectionSchema
        {
            get { return _connectionSchema; }
        }

        /// <summary>
        /// The underlying db connection
        /// </summary>
        public IDbConnection Connection
        {
            get { return _connection; }
        }

        /// <summary>
        /// current avaiable transaction.
        /// this transaction is retrieved from <see cref="LocalTransactionBlock"/> class, and it is created by current connection.
        /// </summary>
        public IDbTransaction Transaction
        {
            get
            {
                var tran = LocalTransactionBlock.GetCurrentTransaction(_connectionSchema.Database);
                if (tran != null && tran.Connection == _connection)
                {
                    return tran;
                }

                return null;
            }
        }

        /// <summary>
        /// 获取命令参数工厂
        /// </summary>
        public IDbParameterFactory ParameterFactory
        {
            get { return this; }
        }

        #endregion

        /// <summary>
        /// Open the connection
        /// </summary>
        private void OpenConnection()
        {
            if (_connection.State == ConnectionState.Closed)
            {
                _openConnectionBySelf = true;
                _connection.Open();
            }
        }

        /// <summary>
        /// This method only close the connection which is opened by this object itself.
        /// </summary>
        private void CloseConnection()
        {
            if (_openConnectionBySelf)
            {
                _connection.Close();
                _openConnectionBySelf = false;
            }
        }

        /// <summary>
        /// 准备命令
        /// </summary>
        /// <param name="sql">指定的sql</param>
        /// <param name="type"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        protected virtual IDbCommand PrepareCommand(string sql, CommandType type, IDbDataParameter[] parameters)
        {
            IDbCommand command = _factory.CreateCommand();
            command.Connection = _connection;
            command.CommandText = sql;
            command.CommandType = type;
            //command.CommandTimeout = _commandTimeout;

            var tran = LocalTransactionBlock.GetCurrentTransaction(_connectionSchema.Database);
            if (tran != null && tran.Connection == _connection)
            {
                command.Transaction = tran;
            }

            var pas = command.Parameters;
            for (int i = 0, c = parameters.Length; i < c; i++)
            {
                var p = parameters[i];
                _sqlDialect.PrepareParameter(p);
                pas.Add(p);
            }
            _sqlDialect.PrepareCommand(command);
            OnDbCommandPrepared(command);
            System.Diagnostics.Trace.WriteLine(command.ToTraceString());
            return command;
        }


        /// <summary>
        /// 执行sql返回受影响行数
        /// </summary>
        /// <param name="sql">指定的sql</param>
        /// <param name="commandType">指定<see cref="System.Data.IDbCommand.CommandText"/>如何被解析</param>
        /// <param name="parameters">sql所包含的参数</param>
        /// <returns>受影响行数</returns>
        public int ExecuteNonQuery(string sql, CommandType commandType, params IDbDataParameter[] parameters)
        {
            IDbCommand command = PrepareCommand(sql, commandType, parameters);
            using (EventTracer.Start("Sql", command))
            {
                try
                {
                    OpenConnection();
                    return command.ExecuteNonQuery();
                }
                catch (Exception exc)
                {
                    OnError(command, exc);
                    throw new SqlException(exc.Message + "\r\n" + command.ToTraceString());
                }
                finally
                {
                    CloseConnection();
                }
            }
        }

        /// <summary>
        /// 执行sql返回结果集第一行第一列，忽略其它值
        /// </summary>
        /// <param name="sql">指定的sql</param>
        /// <param name="type">指定<see cref="System.Data.IDbCommand.CommandText"/>如何被解析</param>
        /// <param name="parameters">sql所包含的参数</param>
        /// <returns><see cref="DBNull"/>或者对象</returns>
        public object ExecuteScalar(string sql, CommandType type, params IDbDataParameter[] parameters)
        {
            IDbCommand command = PrepareCommand(sql, type, parameters);
            using (EventTracer.Start("Sql", command))
            {
                try
                {
                    OpenConnection();
                    return command.ExecuteScalar();
                }
                catch (Exception exc)
                {
                    OnError(command, exc);
                    throw new SqlException(exc.Message + "\r\n" + command.ToTraceString());
                }
                finally
                {
                    CloseConnection();
                }
            }
        }

        /// <summary>
        /// 从关系数据库中查询数据
        /// </summary>
        /// <param name="sql">指定的sql</param>
        /// <param name="type">指定<see cref="System.Data.IDbCommand.CommandText"/>如何被解析</param>
        /// <param name="closeConnection">数据读取器关闭的时候是否关闭链接</param>
        /// <param name="parameters">sql所包含的参数</param>
        /// <returns>数据安全读取器</returns>
        public SafeDataReader ExecuteReader(string sql, CommandType type, bool closeConnection, params IDbDataParameter[] parameters)
        {
            IDbCommand command = PrepareCommand(sql, type, parameters);
            using (EventTracer.Start("Sql", command))
            {
                if (_connection.State == ConnectionState.Closed) { _connection.Open(); }

                IDataReader reader = null;

                try
                {
                    if (closeConnection)
                    {
                        reader = command.ExecuteReader(CommandBehavior.CloseConnection);
                    }
                    else
                    {
                        reader = command.ExecuteReader();
                    }
                }
                catch (Exception exc)
                {
                    OnError(command, exc);
                    throw new SqlException(exc.Message + "\r\n" + command.ToTraceString());
                }

                return new SafeDataReader(reader);
            }
        }

        /// <summary>
        /// 从关系数据库中查询数据并返回<see cref="DataTable"/>
        /// </summary>
        /// <param name="sql">指定的sql</param>
        /// <param name="type">指定<see cref="System.Data.IDbCommand.CommandText"/>如何被解析</param>
        /// <param name="parameters">sql所包含的参数</param>
        /// <returns>数据表</returns>
        public DataTable ExecuteDataTable(string sql, CommandType type, params IDbDataParameter[] parameters)
        {
            IDbDataAdapter da = _factory.CreateDataAdapter();
            da.SelectCommand = PrepareCommand(sql, type, parameters);
            using (EventTracer.Start("Sql", da.SelectCommand))
            {
                DataSet ds = new DataSet();
                try
                {
                    OpenConnection();

                    da.Fill(ds);

                    return ds.Tables[0];
                }
                catch (Exception exc)
                {
                    OnError(da.SelectCommand, exc);
                    throw new SqlException(exc.Message + "\r\n" + da.SelectCommand.ToTraceString());
                }
                finally
                {
                    CloseConnection();
                }
            }
        }

        /// <summary>
        /// 创建数据库命令
        /// </summary>
        /// <param name="sql">指定的sql</param>
        /// <param name="type">指定<see cref="System.Data.IDbCommand.CommandText"/>如何被解析</param>
        /// <param name="parameters">sql所包含的参数</param>
        /// <returns></returns>
        public IDbCommand CreateCommand(string sql, CommandType type, params IDbDataParameter[] parameters)
        {
            return PrepareCommand(sql, type, parameters);
        }

        #region IDbParameterFactory Members

        /// <summary>
        /// Create a DBParameter
        /// </summary>
        /// <returns></returns>
        IDbDataParameter IDbParameterFactory.CreateParameter()
        {
            return _factory.CreateParameter();
        }

        /// <summary>
        /// Create a DBParameter
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        IDbDataParameter IDbParameterFactory.CreateParameter(string name)
        {
            IDbDataParameter para = _factory.CreateParameter();
            para.ParameterName = name;
            return para;
        }

        /// <summary>
        /// Create a DBParameter
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        IDbDataParameter IDbParameterFactory.CreateParameter(string name, object value)
        {
            IDbDataParameter para = _factory.CreateParameter();
            para.ParameterName = name;
            para.Value = value;
            return para;
        }

        /// <summary>
        /// Create a DBParameter
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        IDbDataParameter IDbParameterFactory.CreateParameter(string name, object value, DbType type)
        {
            IDbDataParameter para = _factory.CreateParameter();
            para.ParameterName = name;
            para.DbType = type;
            para.Value = value;
            return para;
        }

        /// <summary>
        /// Create a DBParameter
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        /// <param name="direction"></param>
        /// <returns></returns>
        IDbDataParameter IDbParameterFactory.CreateParameter(string name, object value, ParameterDirection direction)
        {
            IDbDataParameter para = _factory.CreateParameter();
            para.ParameterName = name;
            para.Value = value;
            para.Direction = direction;
            return para;
        }

        /// <summary>
        /// Create a DBParameter
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        /// <param name="type"></param>
        /// <param name="size"></param>
        /// <returns></returns>
        IDbDataParameter IDbParameterFactory.CreateParameter(string name, object value, DbType type, int size)
        {
            IDbDataParameter para = _factory.CreateParameter();
            para.ParameterName = name;
            para.DbType = type;
            para.Size = size;
            para.Value = value;
            return para;
        }

        /// <summary>
        /// Create a DBParameter
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        /// <param name="type"></param>
        /// <param name="direction"></param>
        /// <returns></returns>
        IDbDataParameter IDbParameterFactory.CreateParameter(string name, object value, DbType type, ParameterDirection direction)
        {
            IDbDataParameter para = _factory.CreateParameter();
            para.ParameterName = name;
            para.DbType = type;
            para.Value = value;
            para.Direction = direction;
            return para;
        }

        /// <summary>
        /// Create a DBParameter
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        /// <param name="type"></param>
        /// <param name="size"></param>
        /// <param name="direction"></param>
        /// <returns></returns>
        IDbDataParameter IDbParameterFactory.CreateParameter(string name, object value, DbType type, int size, ParameterDirection direction)
        {
            IDbDataParameter para = _factory.CreateParameter();
            para.ParameterName = name;
            para.DbType = type;
            para.Value = value;
            para.Size = size;
            para.Direction = direction;
            return para;
        }

        #endregion

        #region IDisposable Members

        ~DbAccesser()
        {
            Dispose(false);
        }

        /// <summary>
        /// dispose this accesser.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_connectionCreatedBySelf && _connection != null)
                {
                    _connection.Dispose();
                }
                _connection = null;
                _sqlDialect = null;
                _factory = null;
            }
        }

        #endregion        
    }
}