using System;
using System.Collections.Generic;
using System.Text;

namespace OptKit.Data.SqlTree
{
    /// <summary>
    /// 语法树节点类型。
    /// </summary>
    public enum SqlNodeType
    {
        SqlNodeList,
        SqlLiteral,
        SqlArray,
        SqlSelect,
        SqlTable,
        SqlColumn,
        SqlJoin,
        SqlOrderBy,
        SqlOrderByList,
        SqlSelectAll,
        SqlSubSelect,
        SqlGroupConstraint,
        SqlExistsConstraint,
        SqlNotConstraint,
        SqlGroupBy,
        SqlGroupByList,
        SqlFunction,
        SqlValue,
        SqlBinaryConstraint,
        SqlBinaryFunctioin
    }
}
