using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace Riven.Uow
{
    public class ActiveTransactionInfo
    {
        public virtual IDbTransaction DbTransaction { get; protected set; }

        public virtual IDbConnection DbConnection { get; protected set; }

        public ActiveTransactionInfo(IDbTransaction dbTransaction, IDbConnection dbConnection)
        {
            DbTransaction = dbTransaction;
            DbConnection = dbConnection;
        }
    }
}
