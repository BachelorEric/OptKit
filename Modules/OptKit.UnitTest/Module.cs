using OptKit.Domain;
using OptKit.Modules;
using OptKit.UnitTest;
using System;

[assembly: ModuleInfo(typeof(Module))]
[assembly: DataProvider("UnitTest")]
namespace OptKit.UnitTest
{
    public class Module : IModule
    {
        public void Init(IApp app)
        {
        }

        public void OnTypeFound(ModuleAssembly module, Type type)
        {
        }
    }
}
