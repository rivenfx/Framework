using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Riven.Repositories;
using Riven.Uow;
using Riven.Uow.Providers;
using System.Data;

namespace Riven
{
    public static class RivenUnitOfWorkDapperExtensions
    {
        /// <summary>
        /// 添加 Dapper 实现的 UnitOfWork
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddUnitOfWorkWithDapper(this IServiceCollection services)
        {
            services.TryAddTransient<IDbConnectionProvider, DbConnectionProvider>();

            services.TryAddTransient<IDbConnectionResolver, DbConnectionResolver>();

            services.TryAddTransient<IDapperTransactionStrategy, DapperTransactionStrategy>();

            services.TryAddTransient<IUnitOfWork, DapperUnitOfWork>();

            services.TryAddTransient<IActiveTransactionProvider, DapperActiveTransactionProvider>();

            services.AddRivenUnitOfWork();

            return services;
        }

        ///// <summary>
        ///// 添加 Dapper 实现的 UnitOfWork 支持的仓储
        ///// </summary>
        ///// <param name="services"></param>
        ///// <returns></returns>
        //public static IServiceCollection AddUnitOfWorkWithEntityFrameworkCoreRepository(this IServiceCollection services)
        //{
        //    services.TryAddTransient(typeof(IRepository<>), typeof(EfCoreRepositoryBase<>));
        //    services.TryAddTransient(typeof(IRepository<,>), typeof(EfCoreRepositoryBase<,>));


        //    return services;
        //}


        /// <summary>
        ///  添加 Dapper 实现的 UnitOfWork 默认支持的 DbConnection
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configurationFunc">配置函数</param>
        /// <returns></returns>
        public static IServiceCollection AddUnitOfWorkWithDapperDefaultDbConnection(this IServiceCollection services, Func<DbConnectionConfiguration, IDbConnection> configurationFunc)
        {
            return services.AddUnitOfWorkWithDapperDefaultDbConnection(RivenUnitOfWorkDapperConsts.DefaultDbConnectionProviderName, configurationFunc);
        }


        /// <summary>
        ///  添加 Dapper 实现的 UnitOfWork 支持的 DbConnection
        /// </summary>
        /// <param name="services"></param>
        /// <param name="name">标识名称</param>
        /// <param name="configurationAction">配置函数</param>
        /// <returns></returns>
        public static IServiceCollection AddUnitOfWorkWithDapperDefaultDbConnection(this IServiceCollection services, string name, Func<DbConnectionConfiguration, IDbConnection> configurationFunc)
        {
            Check.NotNullOrWhiteSpace(name, nameof(name));
            Check.NotNull(configurationFunc, nameof(configurationFunc));

            var count = services.Where(o => o.ImplementationInstance is DbConnectionProvider)
                .Select(o => (o.ImplementationInstance as DbConnectionProvider))
                .Count(o => o.Name == name);

            if (count > 0)
            {
                throw new ArgumentException($"A DbConnection with the name {name} already exists");
            }

            var unitOfWorkDbConnectionProvider = new DbConnectionProvider(name, configurationFunc);
            services.AddSingleton<IDbConnectionProvider>(unitOfWorkDbConnectionProvider);

            return services;
        }
    }
}
