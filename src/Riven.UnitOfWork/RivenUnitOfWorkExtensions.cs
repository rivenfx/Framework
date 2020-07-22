using System;
using System.Collections.Generic;
using System.Text;
using Riven.Uow;
using Riven.Uow.Providers;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System.Linq;

namespace Riven
{
    public static class RivenUnitOfWorkExtensions
    {
        /// <summary>
        /// 添加Riven UnitOfWork 服务
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddRivenUnitOfWork(this IServiceCollection services)
        {
            services.TryAddTransient<IUnitOfWorkManager, DefaultUnitOfWorkManager>();
            services.TryAddTransient<ICurrentUnitOfWorkProvider, AsyncLocalCurrentUnitOfWorkProvider>();
            services.TryAddTransient<IActiveTransactionProvider, NullActiveTransactionProvider>();

            services.TryAddTransient<IConnectionStringResolver, DefaultConnectionStringResolver>();
            services.TryAddSingleton<IConnectionStringStore, DefaultConnectionStringStore>();
            services.TryAddScoped<ICurrentConnectionStringName, DefaultCurrentConnectionStringName>();

            return services;
        }


        /// <summary>
        /// 添加默认连接字符
        /// </summary>
        /// <param name="services"></param>
        /// <param name="connectionString">数据库连接字符串</param>
        /// <returns></returns>
        public static IServiceCollection AddDefaultConnectionString(this IServiceCollection services, string connectionString)
        {
            return services.AddConnectionString(RivenUnitOfWorkConsts.DefaultConnectionStringName, connectionString);
        }


        /// <summary>
        /// 添加数据库连接字符串
        /// </summary>
        /// <param name="services"></param>
        /// <param name="name">连接字符串名称</param>
        /// <param name="connectionString">数据库连接字符串</param>
        /// <returns></returns>
        public static IServiceCollection AddConnectionString(this IServiceCollection services, string name, string connectionString)
        {
            Check.NotNullOrEmpty(name, nameof(name));
            Check.NotNullOrEmpty(connectionString, nameof(connectionString));

            var count = services.Where(o => o.ImplementationInstance is IConnectionStringProvider)
                                .Select(o => (o.ImplementationInstance as IConnectionStringProvider))
                                .Count(o => o.Name == name);
            if (count > 0)
            {
                throw new ArgumentException($"A connection string with the name {name} already exists");
            }

            var connectionStringProvider = new ConnectionStringProvider(name, connectionString);
            services.AddSingleton<IConnectionStringProvider>(connectionStringProvider);

            return services;
        }


        /// <summary>
        /// 添加数据库连接字符串
        /// </summary>
        /// <param name="services"></param>
        /// <param name="name">连接字符串名称</param>
        /// <param name="connectionString">数据库连接字符串</param>
        /// <returns></returns>
        public static IServiceProvider AddConnectionString(this IServiceProvider serviceProvider, string name, string connectionString)
        {
            Check.NotNullOrEmpty(name, nameof(name));
            Check.NotNullOrEmpty(connectionString, nameof(connectionString));

            var connectionStringStore = serviceProvider.GetService<IConnectionStringStore>();

            var connectionStringProvider = new ConnectionStringProvider(name, connectionString);
            connectionStringStore.CreateOrUpdate(connectionStringProvider);


            return serviceProvider;
        }
    }
}
