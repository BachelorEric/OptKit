using OptKit.Domain;
using OptKit.Modules;
using OptKit.Primitives.Domain;
using System;
[assembly: ModuleInfo(typeof(OptKit.Primitives.Module))]

namespace OptKit.Primitives
{
    public class Module : IModule
    {
        public void Init(IApp app)
        {
            DomainFactory.SetDomainInterceptor(new DomainInterceptor());
            PropertyContainerFactory.SetFactory(new PropertyContainerFactoryImpl());
        }
    }
}
