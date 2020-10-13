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



        public string Resolve([NotNull]string name)
        {
            Check.NotNullOrWhiteSpace(name, nameof(name));

            var connectionStringProvider = _connectionStringStore.Get(name);
            if (connectionStringProvider != null)
            {
                return connectionStringProvider.ConnectionString;
            }

            if (this._connectionStringProviderDict.TryGetValue(name, out connectionStringProvider))
            {
                return connectionStringProvider.ConnectionString;
            }

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


            throw new ArgumentException($"The connection string with the default name does not exist");
        }
    }
}
