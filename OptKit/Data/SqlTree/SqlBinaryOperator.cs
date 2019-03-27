using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OptKit.Data.SqlTree
{
    /// <summary>
    /// 对比操作符
    /// </summary>
    public enum SqlBinaryOperator
    {
        Equal,
        NotEqual,
        Greater,
        GreaterEqual,
        Less,
        LessEqual,

        Like,
        NotLike,
        Contains,
        NotContains,
        StartsWith,
        NotStartsWith,
        EndsWith,
        NotEndsWith,

        In,
        NotIn,
    }

    /// <summary>
    /// 二位运算类型
    /// </summary>
    public enum SqlGroupOperator
    {
        And,
        Or
    }
}
