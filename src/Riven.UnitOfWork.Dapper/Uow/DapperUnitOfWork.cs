using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Threading;
using Riven.Extensions;
using System.Data;

namespace Riven.Uow
{
    public class DapperUnitOfWork : UnitOfWorkBase
    {
        protected string _connectionProviderName;

        protected readonly IDictionary<string, IDbConnection> _activeConnectionInfo;


        protected readonly IServiceProvider _serviceProvider;
        protected readonly IConnectionStringResolver _connectionStringResolver;
        protected readonly IDbConnectionResolver _dbConnectionResolver;
        protected readonly IDapperTransactionStrategy _transactionStrategy;

        IDictionary<string, IDbConnection> ActiveConnection => this._activeConnectionInfo;

        public DapperUnitOfWork(IConnectionStringResolver connectionStringResolver, IDbConnectionResolver dbConnectionResolver, IDapperTransactionStrategy transactionStrategy)
        {
            _connectionStringResolver = connectionStringResolver;
            _dbConnectionResolver = dbConnectionResolver;
            _transactionStrategy = transactionStrategy;

            _activeConnectionInfo = new Dictionary<string, IDbConnection>();
        }

        /// <summary>
        /// 获取当前的连接和事务信息
        /// </summary>
        /// <returns></returns>
        public virtual ActiveTransactionInfo GetActiveTransactionInfo()
        {
            var nameOrConnectionString = _connectionStringResolver.Resolve(this.GetConnectionStringName());
            return this._transactionStrategy.GetActiveTransactionInfo(nameOrConnectionString);
        }

        /// <summary>
        /// 设置 DbConnection Provider Name
        /// </summary>
        /// <param name="providerName"></param>
        /// <returns></returns>
        public virtual IDisposable SetDbConnectionProvider(string providerName)
        {
            var oldConnectionProviderName = this._connectionProviderName;
            this._connectionProviderName = providerName;

            return new DisposeAction(() =>
            {
                this._connectionProviderName = oldConnectionProviderName;
            });
        }


        protected override void BeginUow()
        {
            if (Options.IsTransactional == true)
            {
                _transactionStrategy.InitOptions(Options);
            }

            this._connectionProviderName = this.Options.GetDbConnectionProviderName();
        }


        public override void SaveChanges()
        {

        }

        public override Task SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return Task.CompletedTask;
        }

        protected override void CompleteUow()
        {
            this.CommitTransaction();
        }

        protected override Task CompleteUowAsync(CancellationToken cancellationToken = default)
        {
            this.CommitTransaction();
            return Task.CompletedTask;
        }

        protected override void DisposeUow()
        {
            foreach (var dapperConnection in GetAllActiveConnection())
            {
                dapperConnection?.Dispose();
            }

            this.ActiveConnection.Clear();
        }



        /// <summary>
        /// 获取所有激活的连接
        /// </summary>
        /// <returns></returns>
        public virtual IReadOnlyList<IDbConnection> GetAllActiveConnection()
        {
            return this.ActiveConnection.Values.ToList().AsReadOnly();
        }


        /// <summary>
        /// 获取或创建连接
        /// </summary>
        /// <returns></returns>
        public virtual IDbConnection GetOrCreateConnection()
        {
            // 获取连接字符串
            var nameOrConnectionString = _connectionStringResolver.Resolve(this.GetConnectionStringName());

            // 缓存键值
            var connectionKey = this.GetConnectionKey(nameOrConnectionString);

            if (!ActiveConnection.TryGetValue(connectionKey, out IDbConnection connection))
            {
                if (Options.IsTransactional == true)
                {
                    connection = this._transactionStrategy.CreateDbConnection(nameOrConnectionString, this._dbConnectionResolver, this._connectionProviderName);
                }
                else
                {
                    connection = this._dbConnectionResolver.Resolve(nameOrConnectionString, this.Options, this._connectionProviderName);
                }

                ActiveConnection[connectionKey] = connection;
            }

            return connection;
        }


        /// <summary>
        /// 获取连接缓存键值
        /// </summary>
        /// <param name="nameOrConnectionString"></param>
        /// <returns></returns>
        protected virtual string GetConnectionKey(string nameOrConnectionString)
        {
            var connectionKey = $"{this._connectionProviderName}#{nameOrConnectionString}";

            return connectionKey;

        }


        /// <summary>
        /// 提交事务
        /// </summary>
        private void CommitTransaction()
        {
            if (Options.IsTransactional == true)
            {
                _transactionStrategy.Commit();
            }
        }
    }
}
