using System;
using System.Collections.Generic;
using System.Text;

namespace OptKit.Domain
{
    public class EntityRepository
    {
        public Entity GetById(object id)
        {
            return null;
        }
        public IEntityList GetByParentId(object parentId)
        {
            return null;
        }
        public void Save(Entity entity)
        {

        }
        public void Save(IEntityList list)
        {

        }
    }
    public class EntityRepository<T> : EntityRepository where T : Entity
    {
        public new T GetById(object id)
        {
            return null;
        }
        //public IEntityList<T> GetByParentId(object parentId)
        //{
        //    return null;
        //}
    }
}
