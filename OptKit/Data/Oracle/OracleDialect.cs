using OptKit.Data.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace OptKit.Data.Oracle
{
    class OracleDialect : ISqlDialect
    {
        public string ProcudureReturnParameterName => throw new NotImplementedException();

        public string DbTimeValueSql()
        {
            throw new NotImplementedException();
        }

        public string GetParameterName(int number)
        {
            throw new NotImplementedException();
        }

        public string LimitIdentifier(string identifier)
        {
            throw new NotImplementedException();
        }

        public void PrepareCommand(IDbCommand command)
        {
            throw new NotImplementedException();
        }

        public string PrepareIdentifier(string identifier)
        {
            throw new NotImplementedException();
        }

        public void PrepareParameter(IDbDataParameter p)
        {
            throw new NotImplementedException();
        }

        public object PrepareValue(object value)
        {
            throw new NotImplementedException();
        }

        public string SelectDbTimeSql()
        {
            throw new NotImplementedException();
        }

        public string SelectSeqNextValueSql(string tableName, string columnName)
        {
            throw new NotImplementedException();
        }

        public string SeqNextValueSql(string tableName, string columnName)
        {
            throw new NotImplementedException();
        }

        public string ToSpecialDbSql(string commonSql)
        {
            throw new NotImplementedException();
        }
    }
}
