using System;

namespace OptKit.Data.SqlTree
{
    /// <summary>
    /// 表示作用于两个操作结点的二位运算结点。
    /// </summary>
    class SqlGroupConstraint : SqlConstraint
    {
        ISqlConstraint _left, _right;

        public override SqlNodeType NodeType { get { return SqlNodeType.SqlGroupConstraint; } }

        public ISqlConstraint Left
        {
            get { return _left; }
            set { _left = value ?? throw new ArgumentNullException("value"); }
        }

        public SqlGroupOperator Opeartor { get; set; }

        public ISqlConstraint Right
        {
            get { return _right; }
            set { _right = value ?? throw new ArgumentNullException("value"); }
        }
    }
}