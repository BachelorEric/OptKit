using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.SelfHost;
using Topshelf;

namespace OptKit.WebApi.SelfHost
{
    class HttpApiService : ServiceControl
    {
        Timer timer;
        HttpSelfHostServer server;

        public HttpApiService()
        {
            var config = new HttpSelfHostConfiguration(RT.Config.Get("HostUrl", "http://localhost:888")); 
            config.Routes.MapHttpRoute("API Default", "api/{controller}/{action}");
            server = new HttpSelfHostServer(config);
        }

        public bool Start(HostControl hostControl)
        {
            RT.Logger.Info("WebApi Host starting...");
            hostControl.RequestAdditionalTime(TimeSpan.FromSeconds(10));
            server.OpenAsync().Wait();
            RegisterToMainHost();
            Console.WriteLine("WebApi Host started");
            RT.Logger.Info("WebApi Host started");
            return true;
        }

        void RegisterToMainHost()
        {
            var section = RT.Config.GetSection("Cluster");
            var masters = section.GetList<string>("MasterHosts");
            if (masters.Any())
            {
                timer = new Timer(e =>
                {
                    foreach (var host in masters)
                    {
                        try
                        {
                            //TODO:Connect to master host
                            RT.Logger.Info("Register:" + host);
                        }
                        catch (Exception exc)
                        {
                            RT.Logger.Error("Register failed:" + host, exc);
                        }
                    }
                }, null, TimeSpan.Zero, TimeSpan.FromMinutes(section.Get<int>("HeartbeatMinutes", 5)));
            }
        }

        public bool Stop(HostControl hostControl)
        {
            server.CloseAsync().Wait();
            server.Dispose();
            return true;
        }
    }
}
