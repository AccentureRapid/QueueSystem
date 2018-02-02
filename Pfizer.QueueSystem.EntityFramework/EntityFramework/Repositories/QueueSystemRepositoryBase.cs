using Abp.Domain.Entities;
using Abp.EntityFramework;
using Abp.EntityFramework.Repositories;

namespace Pfizer.QueueSystem.EntityFramework.Repositories
{
    public abstract class QueueSystemRepositoryBase<TEntity, TPrimaryKey> : EfRepositoryBase<QueueSystemDbContext, TEntity, TPrimaryKey>
        where TEntity : class, IEntity<TPrimaryKey>
    {
        protected QueueSystemRepositoryBase(IDbContextProvider<QueueSystemDbContext> dbContextProvider)
            : base(dbContextProvider)
        {

        }

        //add common methods for all repositories
    }

    public abstract class QueueSystemRepositoryBase<TEntity> : QueueSystemRepositoryBase<TEntity, int>
        where TEntity : class, IEntity<int>
    {
        protected QueueSystemRepositoryBase(IDbContextProvider<QueueSystemDbContext> dbContextProvider)
            : base(dbContextProvider)
        {

        }

        //do not add any method here, add to the class above (since this inherits it)
    }
}
