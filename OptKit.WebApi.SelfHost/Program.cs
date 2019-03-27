using OptKit.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.SelfHost;
using Topshelf;

namespace OptKit.WebApi.SelfHost
{
    class Program
    {
        static void Main(string[] args)
        {
            ConfigManager.Create().UserJsonConfig("appsettings.json");

            RT.ModuleAssemblyFilter = assembly =>
            {
                return !assembly.Contains("OptKit.Schedule");
            };

            AppDomain.CurrentDomain.UnhandledException += (s, e) =>
            {
                Console.WriteLine(e.ExceptionObject?.ToString());
                RT.Logger.Fatal(e.ExceptionObject);
            };

            HostFactory.Run(x =>
            {
                x.Service<HttpApiService>();
                x.SetDescription(RT.Config.Get("ServiceDescription", "OptKit application service"));
                x.SetDisplayName(RT.Config.Get("ServiceDisplayName", "OptKit application service"));
                x.SetInstanceName(RT.Config.Get("ServiceName", "OptKit application service"));
                x.StartAutomaticallyDelayed(); // Automatic (Delayed) -- only available on .NET 4.0 or later
                x.RunAsLocalSystem();
                x.EnableServiceRecovery((e) =>
                {
                    e.RestartService(0);//第一次失败执行
                    e.RestartService(0);//第二次失败执行
                    e.RestartService(0);//后续失败执行
                    e.OnCrashOnly();
                    e.SetResetPeriod(1);
                });
                x.EnablePauseAndContinue();
                x.EnableShutdown();
            });
        }
    }
}
