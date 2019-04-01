using OptKit.Domain;
using OptKit.Domain.Metadata;
using OptKit.UnitTest.PropertyRegister;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace OptKit.xUnit.Domain
{
    public class DomainTest : IClassFixture<AppInit>
    {
        public DomainTest(AppInit app)
        {

        }

        [Fact]
        public void DomainMeta()
        {
            var parent = DomainManager.Domains.Find(typeof(Parent));
            Assert.Equal("父实体", parent.Caption);
            var child = DomainManager.Domains.Find(typeof(Child));
            Assert.Equal("子实体", child.Caption);

            //普通属性
            var name = parent.Property(nameof(Parent.Name));
            Assert.IsType<DataPropertyMeta>(name);
            Assert.NotNull(name?.Property);
            Assert.Equal(nameof(Parent.Name), name.Property.Name);
            //子属性
            var childList = parent.ChildProperty(nameof(Parent.ChildList));
            Assert.IsType<ChildPropertyMeta>(childList);
            Assert.NotNull(childList?.Property);
            Assert.Equal(nameof(Parent.ChildList), childList.Property.Name);
            //引用属性
            var refParent = child.Property(nameof(Child.Parent));
            var refIdParent = child.Property(nameof(Child.ParentId));
            Assert.IsType<RefPropertyMeta>(refParent);
            Assert.Same(refParent, refIdParent);
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

