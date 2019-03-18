using OptKit.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace OptKit.Domain
{
    public class EntityService : RemoteService
    {
        public virtual T GetById<T>(object id) where T : IEntity
        {
            return Activator.CreateInstance<T>();
        }

        public virtual T FindById<T>(object id) where T : IEntity
        {
            return Activator.CreateInstance<T>();
        }
        public virtual T GetByName<T>(string name) where T : INamedEntity
        {
            return Activator.CreateInstance<T>();
        }

        public virtual T FindByName<T>(string name) where T : INamedEntity
        {
            return Activator.CreateInstance<T>();
        }

        public virtual void Save(IEntity entity)
        {

        }
    }
}
