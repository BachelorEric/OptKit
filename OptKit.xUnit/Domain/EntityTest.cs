using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace OptKit.xUnit.Domain
{
    public class EntityTest
    {
        [Fact]
        public void DomainFactory()
        {
            new OptKit.Primitives.Module().Init(null);
            var usr = OptKit.Domain.DomainFactory.Create<User>();
            usr.Name = "Test Name";
        }
    }
}
