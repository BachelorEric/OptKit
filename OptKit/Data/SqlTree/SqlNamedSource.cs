namespace OptKit.Data.SqlTree
{
    /// <summary>
    /// 一个拥有名字、可被引用的数据源。
    /// </summary>
    abstract class SqlNamedSource : SqlSource
    {
        /// <summary>
        /// 获取需要引用本数据源时可用的名字。
        /// </summary>
        /// <returns></returns>
        public abstract string GetName();
    }
}