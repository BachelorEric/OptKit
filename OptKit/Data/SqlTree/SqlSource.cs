namespace OptKit.Data.SqlTree
{
    /// <summary>
    /// 可查询的数据源，可用于 From 语句之后 。
    /// 目前有：SqlTable、SqlJoin、SqlSubSelect。
    /// </summary>
    abstract class SqlSource : SqlNode, ISqlSource
    {
    }
}
