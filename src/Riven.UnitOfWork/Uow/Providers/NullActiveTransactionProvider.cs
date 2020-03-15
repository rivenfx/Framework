using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Riven.Uow.Providers
{
    public class NullActiveTransactionProvider : IActiveTransactionProvider
    {
        public IDbConnection GetActiveConnection()
        {
            throw new NotImplementedException();
        }

        public IDbTransaction GetActiveTransaction()
        {
            throw new NotImplementedException();
        }
    }
}
