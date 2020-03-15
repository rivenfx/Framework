using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Riven.Extensions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Riven.Uow.Providers
{
    public class EfCoreActiveTransactionProvider : IActiveTransactionProvider
    {
        protected readonly ICurrentUnitOfWorkProvider _currentUnitOfWorkProvider;

        public EfCoreActiveTransactionProvider(ICurrentUnitOfWorkProvider currentUnitOfWorkProvider)
        {
            _currentUnitOfWorkProvider = currentUnitOfWorkProvider;
        }

        public IDbConnection GetActiveConnection()
        {
            return GetDbContext().Database.GetDbConnection();
        }

        public IDbTransaction GetActiveTransaction()
        {
            return GetDbContext().Database.CurrentTransaction?.GetDbTransaction();
        }

        protected virtual DbContext GetDbContext()
        {
            return _currentUnitOfWorkProvider.Current.GetDbContext();
        }
    }
}
