using System;
using System.Collections.Generic;
using System.Text;

namespace OptKit.Data.SqlTree
{
    public interface ISqlNode
    {
        SqlNodeType NodeType { get; }
    }
    public interface ISqlValue : ISqlNode { }
    public interface ISqlSelect : ISqlNode { }
    public interface ISqlConstraint : ISqlNode { }
    public interface ISqlSource : ISqlNode { }
    public interface IOrderBy : ISqlNode { }
    public interface IGroupBy : ISqlNode { }
    public interface ISqlLiteral : ISqlSelect, ISqlConstraint, ISqlSource { }
}
