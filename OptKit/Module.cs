using OptKit;
using OptKit.Domain;
using OptKit.Modules;
using System;
using System.Collections.Generic;
using System.Text;

[assembly: ModuleInfo(typeof(Module))]
namespace OptKit
{
    class Module : ServiceModule
    {
        public override void Init(IApp app) { }

        public override void OnTypeFound(ModuleAssembly module, Type type)
        {
            DomainManager.Domains.InitConfig(module, type);
        }
    }
}
