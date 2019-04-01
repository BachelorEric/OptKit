namespace OptKit.Diagnostics
{
    /// <summary>
    /// 事件跟踪监听器
    /// </summary>
    public abstract class EventTracerListener
    {
        /// <summary>
        /// 写入事件
        /// </summary>
        /// <param name="caption">标题</param>
        /// <param name="context">上下文</param>
        /// <param name="elapsed">耗时（毫秒）</param>
        public abstract void Write(string caption, object context, long elapsed);
    }
}
