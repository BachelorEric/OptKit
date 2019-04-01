using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace OptKit.Domain
{
    public class EntityRepositoryFactory
    {
        const string RepositorySuffix = "Repository";
        const string DbMaster = "Master";
        static IDictionary<Type, EntityRepository> _repositorys = new Dictionary<Type, EntityRepository>();

        public static EntityRepository Find(Type entityType)
        {
            Check.NotNull(entityType, nameof(entityType));
            if (!typeof(IEntity).IsAssignableFrom(entityType))
                throw new ArgumentException("[{0}]未实现IEntity".FormatArgs(entityType.GetQualifiedName()), nameof(entityType));
            EntityRepository result;
            if (!_repositorys.TryGetValue(entityType, out result))
            {
                lock (_repositorys)
                {
                    if (!_repositorys.TryGetValue(entityType, out result))
                    {
                        var repoType = RepositoryForEntity(entityType);
                        result = Activator.CreateInstance(repoType) as EntityRepository;
                        var dataProvider = repoType.GetCustomAttribute<DataProviderAttribute>();
                        if (dataProvider == null)
                            dataProvider = repoType.Assembly.GetCustomAttribute<DataProviderAttribute>();
                        result.ConnectionStringName = dataProvider?.ConnectionStringName ?? DbMaster;
                        _repositorys.Add(entityType, result);
                    }
                }
            }
            return result;
        }

        static Type RepositoryForEntity(Type entityType)
        {
            string rpName = entityType.FullName + RepositorySuffix;
            Type rpType = entityType.Assembly.GetType(rpName);
            if (rpType != null)
                return rpType;
            return typeof(EntityRepository<>).MakeGenericType(entityType);
        }

        public static EntityRepository Find<T>() where T : IEntity
        {
            return Find(typeof(T));
        }
    }
    public class RF : EntityRepositoryFactory { }
}
