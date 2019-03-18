namespace OptKit.Services
{
    /// <summary>
    /// 服务容器接口。注册或者解析服务。
    /// </summary>
    public interface IServiceContainer : IServiceRegistrar, IServiceResolver
    {
    }
}
