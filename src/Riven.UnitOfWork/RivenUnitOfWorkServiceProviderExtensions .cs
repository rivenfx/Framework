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
    public static class RivenUnitOfWorkServiceProviderExtensions
    {
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
