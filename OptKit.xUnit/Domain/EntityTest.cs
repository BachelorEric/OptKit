using OptKit.Domain;
using OptKit.UnitTest;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace OptKit.xUnit.Domain
{
    public class EntityTest : IClassFixture<AppInit>
    {
        public EntityTest(AppInit app)
        {

        }

        [Fact]
        public void ModelProperty()
        {
            var m = Entity.Create<ModelB>();
            m.Name = "NewName";
            m.Qty = 1.2;
            Assert.Equal("NewName", m.Name);
            Assert.Equal(1.2, m.Qty);
        }

        public void ParentChild()
        {

        }
    }
}
