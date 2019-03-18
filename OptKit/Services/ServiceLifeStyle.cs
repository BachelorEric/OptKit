namespace OptKit.Services
{
    /// <summary>
    /// 依赖注入系统中类型的生活方式
    /// </summary>
    public enum ServiceLifeStyle
    {
        /// <summary>
        /// 单例对象。在第一次解析时创建单个对象，以后的解析都返回此对象。
        /// </summary>
        Singleton,

        /// <summary>
        /// 短暂的对象。每次解析都创建一个新对象。
        /// </summary>
        Transient
    }
}
