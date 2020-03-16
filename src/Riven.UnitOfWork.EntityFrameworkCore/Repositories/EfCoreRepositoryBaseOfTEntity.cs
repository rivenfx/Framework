using Riven.Entities;
using Riven.Uow.Providers;
using System;
using System.Collections.Generic;
using System.Text;

namespace Riven.Repositories
{
    public class EfCoreRepositoryBase<TEntity> : EfCoreRepositoryBase<TEntity, long>, IRepository<TEntity>
           where TEntity : class, IEntity<long>
    {
        public EfCoreRepositoryBase(IActiveTransactionProvider transactionProvider, IUnitOfWorkDbContextProvider unitOfWorkDbContextProvider)
            : base(transactionProvider, unitOfWorkDbContextProvider)
        {
        }
    }
}
