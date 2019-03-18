using Castle.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace OptKit.Services
{
    /// <summary>
    /// Remote service. The service methods from <see cref="RemoteService"/> or subclass of <see cref="RemoteService"/> will run at remote server
    /// except which the method is mark <see cref="LocalAttribute"/>.
    /// The instance of the <see cref="RemoteService"/> is Singleton should be created from the <see cref="IServiceContainer"/>.
    /// eg RT.Service.Resolve&lt;EntityService&gt;(). 
    /// All service method should be virtual, so the AOP framework will create the proxy method from it.
    /// The assembly contains remote serive should be mark [assembly: InternalsVisibleTo("DynamicProxyGenAssembly2")].
    /// </summary>
    [Interceptor(typeof(RemoteServiceInterceptor))]
    public class RemoteService
    {
    }
}
