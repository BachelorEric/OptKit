using OptKit.Configuration;
using OptKit.Runtime;
using System;
using System.Collections.Generic;
using System.Text;

namespace OptKit.xUnit
{
    public class AppInit
    {
        ServerApp app;
        public AppInit()
        {
            ConfigManager.Create().UserJsonConfig("appsettings.json");
            app = new ServerApp();
            app.Startup();
        }

        ~AppInit()
        {
            app.Stop();
        }
    }
}
