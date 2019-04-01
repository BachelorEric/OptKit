using OptKit.Domain;
using OptKit.UnitTest.ClrProperty;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace OptKit.xUnit.Domain
{
    public class ClrDomainTest : IClassFixture<AppInit>
    {
        public ClrDomainTest(AppInit app)
        {

        }

        [Fact]
        public void ParentChild()
        {
            var m = Entity.Create<Parent>();
            m.Name = "NewName";
            Assert.Equal("NewName", m.Name);
        }
    }
}
