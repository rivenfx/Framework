using Riven.Extensions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Riven.Uow
{
    public interface IDapperTransactionStrategy : IDisposable
    {
        /// <summary>
        /// 初始化工作单元选项
        /// </summary>
        /// <param name="options"></param>
        void InitOptions(UnitOfWorkOptions options);


        /// <summary>
        /// 获取当前的连接/事务,没有则为 null 
        /// </summary>
        /// <param name="connectionString"></param>
        /// <returns></returns>
        ActiveTransactionInfo GetActiveTransactionInfo(string connectionString);


        /// <summary>
        /// 创建数据库连接对象
        /// </summary>
        /// <param name="connectionString">连接字符串</param>
        /// <param name="connectionResolver">数据库上下文实例化器</param>
        /// <param name="connectionProviderName">连接标识名称</param>
        /// <returns></returns>
        IDbConnection CreateDbConnection(string connectionString, IDbConnectionResolver connectionResolver, string connectionProviderName);


        /// <summary>
        /// 提交
        /// </summary>
        void Commit();
    }

    public class DapperTransactionStrategy : IDapperTransactionStrategy
    {
        protected UnitOfWorkOptions Options { get; private set; }
        protected IDictionary<string, ActiveTransactionInfo> ActiveTransactions { get; }


        public DapperTransactionStrategy()
        {
            ActiveTransactions = new Dictionary<string, ActiveTransactionInfo>();
        }

        public void InitOptions(UnitOfWorkOptions options)
        {
            this.Options = options;
        }

        public virtual ActiveTransactionInfo GetActiveTransactionInfo(string connectionString)
        {
            var activeTransaction = ActiveTransactions.GetOrDefault(connectionString);
            return activeTransaction;
        }

        public virtual IDbConnection CreateDbConnection(string connectionString, IDbConnectionResolver connectionResolver, string connectionProviderName)
        {
            IDbConnection dbConnection = null;

            var activeTransaction = this.GetActiveTransactionInfo(connectionString);
            if (activeTransaction == null)
            {
                dbConnection = connectionResolver.Resolve(connectionString, this.Options, connectionProviderName);
                var dbTransaction = dbConnection.BeginTransaction(
                    (Options.IsolationLevel ?? System.Transactions.IsolationLevel.ReadUncommitted).ToSystemDataIsolationLevel()
                    );
                activeTransaction = new ActiveTransactionInfo(dbTransaction, dbConnection);
                ActiveTransactions[connectionString] = activeTransaction;
            }

            return dbConnection;
        }


        public virtual void Commit()
        {
            foreach (var activeTransaction in ActiveTransactions.Values)
            {
                activeTransaction.DbTransaction.Commit();
            }
        }


        public void Dispose()
        {
            this.Dispose(true);
        }

        protected virtual void Dispose(bool state)
        {
            foreach (var activeTransaction in ActiveTransactions.Values)
            {
                activeTransaction.DbTransaction?.Dispose();
                activeTransaction.DbConnection?.Dispose();
            }

            ActiveTransactions.Clear();
        }
    }
}
