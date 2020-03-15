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

        public DefaultConnectionStringResolver(IServiceProvider service)
        {
            _connectionStringProviderDict = service.GetServices<IConnectionStringProvider>()
                .ToDictionary(o => o.Name);
        }
        public string Resolve(string name)
        {
            if (this._connectionStringProviderDict.TryGetValue(name, out IConnectionStringProvider connectionStringProvider))
            {
                return connectionStringProvider.ConnectionString;
            }

            throw new ArgumentException($"The connection string with the name {name} does not exist");
        }
    }
}
