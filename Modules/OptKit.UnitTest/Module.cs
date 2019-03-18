using OptKit.Modules;
using OptKit.UnitTest;
using System;

[assembly: ModuleInfo(typeof(Module))]
namespace OptKit.UnitTest
{
    public class Module : IModule
    {
        public void Init(IApp app)
        {
        }
    }
}
