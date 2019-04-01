using OptKit.Domain.Mapping;
using System;
using System.Collections.Generic;
using System.Text;

namespace OptKit.Domain
{
    public class EntityRepository
    {

        protected RdbTable RdbTable { get; }

        public string ConnectionStringName { get; internal set; }

        public Entity GetById(object id)
        {
            return null;
        }
        public IDomainList GetByParentId(object parentId)
        {
            return null;
        }
        public void Save(Entity entity)
        {

        }
        public void Save(IDomainList list)
        {

        }
    }
    public class EntityRepository<T> : EntityRepository where T : Entity
    {
    }
}
