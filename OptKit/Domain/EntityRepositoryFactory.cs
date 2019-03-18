using System;
using System.Collections.Generic;
using System.Text;

namespace OptKit.Domain
{
    public class EntityRepositoryFactory
    {
        public static EntityRepository Find(Type entityType)
        {
            return null;
        }
        public static EntityRepository Find<T>()
        {
            return null;
        }
    }
    public class RF : EntityRepositoryFactory { }
}
