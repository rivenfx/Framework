using Microsoft.Extensions.DependencyInjection;
using Riven.Extensions;
using Riven.Uow;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Riven
{
    public interface IDbConnectionResolver
    {
        /// <summary>
        /// 获取数据库连接
        /// </summary>
        /// <param name="connectionString"></param>
        /// <param name="unitOfWorkOptions"></param>
        /// <param name="connectionProviderName"></param>
        /// <returns></returns>
        IDbConnection Resolve(string connectionString, UnitOfWorkOptions unitOfWorkOptions, string connectionProviderName);
    }

    public class DbConnectionResolver : IDbConnectionResolver
    {
        protected readonly Dictionary<string, IDbConnectionProvider> _dbConnectionProviderDict;

        public DbConnectionResolver(IServiceProvider service)
        {
            _dbConnectionProviderDict = service.GetServices<IDbConnectionProvider>()
                .ToDictionary(o => o.Name);
        }

        public IDbConnection Resolve(string connectionString, UnitOfWorkOptions unitOfWorkOptions, string connectionProviderName)
        {
            var provider = this.GetDbConnectionProvider(connectionProviderName);

            var dbConnectionConfiguration = new DbConnectionConfiguration(connectionString, unitOfWorkOptions);

            return provider.Configuration?.Invoke(dbConnectionConfiguration);
        }


        #region 私有函数

        /// <summary>
        /// 根据DbConnection Name 来寻找对应的 provider
        /// </summary>
        /// <param name="providerName"></param>
        /// <returns></returns>
        private IDbConnectionProvider GetDbConnectionProvider(string providerName)
        {
            if (_dbConnectionProviderDict.TryGetValue(providerName, out IDbConnectionProvider dbConnectionProvider))
            {
                return dbConnectionProvider;
            }

            if (providerName.IsNullOrWhiteSpace())
            {
                throw new ArgumentOutOfRangeException("The default DbConnection is not registered");
            }

            throw new ArgumentOutOfRangeException($"The DbConnection with the name {providerName} is not registered");
        }
        #endregion
    }
}
