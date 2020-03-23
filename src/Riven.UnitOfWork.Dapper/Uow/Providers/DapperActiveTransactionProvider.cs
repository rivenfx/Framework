using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Riven.Uow.Providers
{
    public class DapperActiveTransactionProvider : IActiveTransactionProvider
    {
        protected readonly ICurrentUnitOfWorkProvider _currentUnitOfWorkProvider;

        public DapperActiveTransactionProvider(ICurrentUnitOfWorkProvider currentUnitOfWorkProvider)
        {
            _currentUnitOfWorkProvider = currentUnitOfWorkProvider;
        }

        public IDbConnection GetActiveConnection()
        {
            return this.GetActiveTransactionInfo().DbConnection;
        }

        public IDbTransaction GetActiveTransaction()
        {
            return this.GetActiveTransactionInfo().DbTransaction;
        }

        protected virtual ActiveTransactionInfo GetActiveTransactionInfo()
        {
            return (_currentUnitOfWorkProvider.Current as DapperUnitOfWork).GetActiveTransactionInfo();
        }
    }
}
