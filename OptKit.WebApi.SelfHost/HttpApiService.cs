using OptKit.Runtime;
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
        ServerApp app;

        public HttpApiService()
        {
            var config = new HttpSelfHostConfiguration(RT.Config.Get("OptKit.HostUrl", "http://localhost:888"));
            config.Routes.MapHttpRoute("API Default", "api/{controller}/{action}");
            server = new HttpSelfHostServer(config);
            app = new ServerApp();
        }

        public bool Start(HostControl hostControl)
        {
            app.Logger.Info("WebApi Host starting...");
            hostControl.RequestAdditionalTime(TimeSpan.FromSeconds(10));
            app.Startup();
            server.OpenAsync().Wait();
            RegisterToMainHost();
            Console.WriteLine("WebApi Host started");
            app.Logger.Info("WebApi Host started");
            return true;
        }

        void RegisterToMainHost()
        {
            var section = RT.Config.GetSection("OptKit.Cluster");
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
            app.Stop();
            return true;
        }
    }
}
