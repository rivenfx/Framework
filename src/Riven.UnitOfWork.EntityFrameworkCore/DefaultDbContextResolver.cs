using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Reflection;
using System.Text;
using System.Linq;
using Riven.Uow;
using Riven.Extensions;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Concurrent;

namespace Riven
{
    /// <summary>
    /// DbContext 创建器
    /// </summary>
    public class DefaultDbContextResolver : IDbContextResolver
    {
        protected static ConcurrentDictionary<Type, ConstructorInfo> _dbContextConstructorDict = new ConcurrentDictionary<Type, ConstructorInfo>();

        protected readonly IServiceProvider _serviceProvider;

        protected readonly IDbContextProviderStorage _dbContextProviderStorage;

        protected readonly IEFCoreDbContextModelStorage _contextModelStorage;

        public DefaultDbContextResolver(IServiceProvider serviceProvider, IDbContextProviderStorage dbContextProviderStorage, IEFCoreDbContextModelStorage contextModelStorage)
        {
            _serviceProvider = serviceProvider;
            _dbContextProviderStorage = dbContextProviderStorage;
            _contextModelStorage = contextModelStorage;
        }



        #region 获取的实现

        /// <inheritdoc/>
        public TDbContext Resolve<TDbContext>(string connectionString, DbConnection existingConnection, UnitOfWorkOptions unitOfWorkOptions, string dbContextProviderName) where TDbContext : DbContext
        {
            return this.Resolve(connectionString, existingConnection, unitOfWorkOptions, dbContextProviderName) as TDbContext;
        }

        /// <inheritdoc/>
        public DbContext Resolve(string connectionString, DbConnection existingConnection, UnitOfWorkOptions unitOfWorkOptions, string dbContextProviderName)
        {
            // 获取 DbContextProvider
            var dbContextProvider = this.GetDbContextProvider(dbContextProviderName);

            // 创建配置器
            var dbContextConfiguration = new DbContextConfiguration(connectionString, existingConnection, unitOfWorkOptions);
            dbContextProvider.Configuration?.Invoke(dbContextConfiguration);

            // 附加模型
            var model = _contextModelStorage.Get(connectionString);
            if (model != null)
            {
                dbContextConfiguration.DbContextOptions.UseModel(model);
            }

            // 实例化对象
            var constructor = this.GetDbContextConstructor(dbContextProvider, dbContextConfiguration);
            var obj = constructor.Invoke(new object[] {
                dbContextConfiguration.DbContextOptions.Options,
                this._serviceProvider
            });


            return (DbContext)obj;
        }


        #endregion


        #region 私有函数

        /// <summary>
        /// 根据DbContext Name 来寻找对应的 provider
        /// </summary>
        /// <param name="providerName"></param>
        /// <returns></returns>
        private IDbContextProvider GetDbContextProvider(string providerName)
        {
            var dbContextProvider = _dbContextProviderStorage.Get(providerName);

            if (dbContextProvider != null)
            {
                return dbContextProvider;
            }

            if (providerName.IsNullOrWhiteSpace())
            {
                throw new ArgumentOutOfRangeException("The default DbContext is not registered");
            }

            throw new ArgumentOutOfRangeException($"The DbContext with the name {providerName} is not registered");
        }

        /// <summary>
        /// 获取 DbContext 构造函数
        /// </summary>
        /// <param name="dbContextProvider"></param>
        /// <param name="dbContextConfiguration"></param>
        /// <returns></returns>
        private ConstructorInfo GetDbContextConstructor(IDbContextProvider dbContextProvider, DbContextConfiguration dbContextConfiguration)
        {
            if (_dbContextConstructorDict.TryGetValue(dbContextProvider.DbContextType, out ConstructorInfo constructor))
            {
                return constructor;
            }

            var constructors = dbContextProvider.DbContextType.GetConstructors();
            constructor = constructors.Where(o =>
            {
                var parameterInfos = o.GetParameters();

                var dbContextOptionsParameter = parameterInfos
                          .FirstOrDefault(parameter => parameter.ParameterType == dbContextConfiguration.DbContextOptions.Options.GetType().BaseType);

                return dbContextOptionsParameter != null;

            }).FirstOrDefault();

            if (constructor == null)
            {
                throw new ArgumentException($"The DbContextType {dbContextProvider.DbContextType.FullName} does not have a constructor with a DbContextOptions argument");
            }

            _dbContextConstructorDict[dbContextProvider.DbContextType] = constructor;
            return constructor;
        }

        #endregion
    }
}
