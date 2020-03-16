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

        protected readonly IConnectionStringStore _connectionStringStore;

        public DefaultConnectionStringResolver(IServiceProvider service, IConnectionStringStore connectionStringStore)
        {
            _connectionStringProviderDict = service.GetServices<IConnectionStringProvider>()
                .ToDictionary(o => o.Name);
            _connectionStringStore = connectionStringStore;
        }



        public string Resolve(string name)
        {
            var connectionStringProvider = _connectionStringStore.Get(name);
            if (connectionStringProvider != null)
            {
                return connectionStringProvider.ConnectionString;
            }

            if (this._connectionStringProviderDict.TryGetValue(name, out connectionStringProvider))
            {
                return connectionStringProvider.ConnectionString;
            }


            throw new ArgumentException($"The connection string with the name {name} does not exist");
        }
    }
}
