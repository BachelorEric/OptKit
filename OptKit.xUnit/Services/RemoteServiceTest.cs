using OptKit.Configuration;
using OptKit.Rbac;
using OptKit.UnitTest;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace OptKit.xUnit.Services
{
    public class RemoteServiceTest : IClassFixture<ConfigInit>
    {
        protected ConfigInit ConfigInit { get; set; }

        public RemoteServiceTest(ConfigInit config)
        {
            ConfigInit = config;
        }

        [Fact]
        public void VoidMethod()
        {
            var service = RT.Service.Resolve<TestService>();
            service.VoidMethod();
        }

        [Fact]
        public void ReturnWithArgumentsMethod()
        {
            var service = RT.Service.Resolve<TestService>();
            var result = service.ReturnWithArgumentsMethod("string", 3, null);
        }

        [Fact]
        public void GenericMethod()
        {
            var service = RT.Service.Resolve<TestService>();
            var result = service.GenericMethod("string");
        }

        [Fact]
        public void GenericVoidMethod()
        {
            var service = RT.Service.Resolve<TestService>();
            service.GenericVoidMethod("string");
        }
    }
}
