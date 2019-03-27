using System;

namespace OptKit.Data.SqlTree
{
    /// <summary>
    /// SqlNode 语法树的访问器
    /// </summary>
    abstract class SqlNodeVisitor
    {
        protected virtual ISqlNode Visit(ISqlNode node)
        {
            switch (node.NodeType)
            {
                case SqlNodeType.SqlLiteral:
                    return VisitSqlLiteral(node as SqlLiteral);
                case SqlNodeType.SqlNodeList:
                    return VisitSqlNodeList(node as SqlNodeList);
                case SqlNodeType.SqlSelect:
                    return VisitSqlSelect(node as SqlSelect);
                case SqlNodeType.SqlColumn:
                    return VisitSqlColumn(node as SqlColumn);
                case SqlNodeType.SqlTable:
                    return VisitSqlTable(node as SqlTable);
                case SqlNodeType.SqlGroupConstraint:
                    return VisitSqlGroupConstraint(node as SqlGroupConstraint);
                case SqlNodeType.SqlJoin:
                    return VisitSqlJoin(node as SqlJoin);
                case SqlNodeType.SqlArray:
                    return VisitSqlArray(node as SqlArray);
                case SqlNodeType.SqlSelectAll:
                    return VisitSqlSelectAll(node as SqlSelectAll);
                case SqlNodeType.SqlExistsConstraint:
                    return VisitSqlExistsConstraint(node as SqlExistsConstraint);
                case SqlNodeType.SqlNotConstraint:
                    return VisitSqlNotConstraint(node as SqlNotConstraint);
                case SqlNodeType.SqlSubSelect:
                    return VisitSqlSubSelect(node as SqlSubSelect);
                case SqlNodeType.SqlOrderBy:
                    return VisitSqlOrderBy(node as SqlOrderBy);
                case SqlNodeType.SqlOrderByList:
                    return VisitSqlOrderByList(node as SqlOrderByList);
                case SqlNodeType.SqlGroupBy:
                    return VisitSqlGroupBy(node as SqlGroupBy);
                case SqlNodeType.SqlGroupByList:
                    return VisitSqlGroupByList(node as SqlGroupByList);
                case SqlNodeType.SqlFunction:
                    return VisitSqlFunction(node as SqlFunction);
                case SqlNodeType.SqlBinaryFunctioin:
                    return VisitSqlBinaryFunction(node as SqlBinaryFunction);
                case SqlNodeType.SqlValue:
                    return VisitSqlValue(node as SqlValue);
                case SqlNodeType.SqlBinaryConstraint:
                    return VisitSqlBinaryConstraint(node as SqlBinaryConstraint);
                default:
                    break;
            }
            throw new NotImplementedException();
        }

        protected virtual SqlBinaryConstraint VisitSqlBinaryConstraint(SqlBinaryConstraint node)
        {
            Visit(node.Left);
            VisitBinaryOperator(node.Operator);
            Visit(node.Right);
            return node;
        }

        protected virtual SqlBinaryOperator VisitBinaryOperator(SqlBinaryOperator op)
        {
            return op;
        }

        protected virtual SqlValue VisitSqlValue(SqlValue node)
        {
            return node;
        }

        protected virtual SqlBinaryFunction VisitSqlBinaryFunction(SqlBinaryFunction node)
        {
            return node;
        }

        protected virtual SqlFunction VisitSqlFunction(SqlFunction node)
        {
            return node;
        }

        protected virtual SqlNode VisitSqlNodeList(SqlNodeList sqlNodeList)
        {
            for (int i = 0, c = sqlNodeList.Items.Count; i < c; i++)
            {
                var item = sqlNodeList.Items[i];
                if (item != null)
                {
                    Visit(item);
                }
            }
            return sqlNodeList;
        }

        protected virtual SqlJoin VisitSqlJoin(SqlJoin sqlJoin)
        {
            Visit(sqlJoin.Left);
            Visit(sqlJoin.Right);
            Visit(sqlJoin.Condition);
            return sqlJoin;
        }

        protected virtual SqlGroupConstraint VisitSqlGroupConstraint(SqlGroupConstraint node)
        {
            Visit(node.Left);
            VisitGroupOperator(node.Opeartor);
            Visit(node.Right);
            return node;
        }

        protected virtual SqlGroupOperator VisitGroupOperator(SqlGroupOperator op)
        {
            return op;
        }

        protected virtual SqlSelect VisitSqlSelect(SqlSelect sqlSelect)
        {
            if (sqlSelect.Selection != null)
            {
                Visit(sqlSelect.Selection);
            }
            Visit(sqlSelect.From);
            if (sqlSelect.Where != null)
            {
                Visit(sqlSelect.Where);
            }
            if (sqlSelect.HasGroup())
            {
                for (int i = 0, c = sqlSelect.GroupBy.Count; i < c; i++)
                {
                    var item = sqlSelect.GroupBy.Items[i] as SqlNode;
                    Visit(item);
                }
            }
            if (sqlSelect.HasOrdered())
            {
                for (int i = 0, c = sqlSelect.OrderBy.Count; i < c; i++)
                {
                    var item = sqlSelect.OrderBy.Items[i] as SqlNode;
                    Visit(item);
                }
            }
            return sqlSelect;
        }

        protected virtual SqlTable VisitSqlTable(SqlTable sqlTable)
        {
            return sqlTable;
        }

        protected virtual SqlColumn VisitSqlColumn(SqlColumn sqlColumn)
        {
            return sqlColumn;
        }

        protected virtual SqlLiteral VisitSqlLiteral(SqlLiteral sqlLiteral)
        {
            return sqlLiteral;
        }

        protected virtual SqlArray VisitSqlArray(SqlArray sqlArray)
        {
            for (int i = 0, c = sqlArray.Items.Count; i < c; i++)
            {
                var item = sqlArray.Items[i] as SqlNode;
                Visit(item);
            }
            return sqlArray;
        }

        protected virtual SqlSelectAll VisitSqlSelectAll(SqlSelectAll sqlSelectStar)
        {
            return sqlSelectStar;
        }

        protected virtual SqlExistsConstraint VisitSqlExistsConstraint(SqlExistsConstraint sqlExistsConstraint)
        {
            Visit(sqlExistsConstraint.Select);
            return sqlExistsConstraint;
        }

        protected virtual SqlNotConstraint VisitSqlNotConstraint(SqlNotConstraint sqlNotConstraint)
        {
            Visit(sqlNotConstraint.Constraint);
            return sqlNotConstraint;
        }

        protected virtual SqlSubSelect VisitSqlSubSelect(SqlSubSelect subSelect)
        {
            Visit(subSelect.Select);
            return subSelect;
        }

        protected virtual SqlOrderBy VisitSqlOrderBy(SqlOrderBy sqlOrderBy)
        {
            return sqlOrderBy;
        }

        protected virtual SqlOrderByList VisitSqlOrderByList(SqlOrderByList sqlOrderByList)
        {
            foreach (SqlOrderBy item in sqlOrderByList.Items)
            {
                Visit(item);
            }
            return sqlOrderByList;
        }

        protected virtual SqlGroupBy VisitSqlGroupBy(SqlGroupBy sqlGroupBy)
        {
            return sqlGroupBy;
        }

        protected virtual SqlGroupByList VisitSqlGroupByList(SqlGroupByList sqlGroupByList)
        {
            foreach (SqlGroupBy item in sqlGroupByList.Items)
            {
                Visit(item);
            }
            return sqlGroupByList;
        }
    }
}