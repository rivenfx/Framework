using JetBrains.Annotations;

using Microsoft.Extensions.DependencyInjection;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Riven.Uow
{
    public class DefaultConnectionStringResolver : IConnectionStringResolver
    {
        protected readonly Dictionary<string, IConnectionStringProvider> _connectionStringProviderDict;

        protected readonly IConnectionStringStorage _connectionStringStore;

        public DefaultConnectionStringResolver(IServiceProvider service, IConnectionStringStorage connectionStringStore)
        {
            _connectionStringProviderDict = service.GetServices<IConnectionStringProvider>()
                .ToDictionary(o => o.Name);
            _connectionStringStore = connectionStringStore;
        }



        public string Resolve(string name)
        {
            // 如果输入为空，那么使用默认的连接字符串键值
            if (string.IsNullOrWhiteSpace(name))
            {
                name = RivenUnitOfWorkConsts.DefaultConnectionStringName;
            }


            var connectionStringProvider = _connectionStringStore.Get(name);
            if (connectionStringProvider != null)
            {
                return connectionStringProvider.ConnectionString;
            }

            if (this._connectionStringProviderDict.TryGetValue(name, out connectionStringProvider))
            {
                return connectionStringProvider.ConnectionString;
            }


            // 如果输入的键值未找到指定的连接字符串,那么使用系统默认的连接字符串键值
            name = RivenUnitOfWorkConsts.DefaultConnectionStringName;

            connectionStringProvider = _connectionStringStore.Get(name);
            if (connectionStringProvider != null)
            {
                return connectionStringProvider.ConnectionString;
            }

            if (this._connectionStringProviderDict.TryGetValue(name, out connectionStringProvider))
            {
                return connectionStringProvider.ConnectionString;
            }

            // 系统默认连接字符串找不到则抛出异常
            throw new ArgumentException($"The connection string with the default name does not exist");
        }
    }
}
