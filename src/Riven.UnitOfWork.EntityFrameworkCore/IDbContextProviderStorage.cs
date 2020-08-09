using Microsoft.Extensions.DependencyInjection;

using Riven.Storage;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Riven
{
    /// <summary>
    /// DbContext Provider Storage
    /// </summary>
    public interface IDbContextProviderStorage : IAnyStorage<IDbContextProvider>
    {
    }

    public class DefaultDbContextProviderStorage : AnyStorageBase<IDbContextProvider>, IDbContextProviderStorage
    {
        public DefaultDbContextProviderStorage(IServiceProvider serviceProvider)
        {
            var dbContextProviders = serviceProvider.GetServices<IDbContextProvider>()
                .ToDictionary(o => o.Name);
            foreach (var item in dbContextProviders)
            {
                this.AddOrUpdate(item.Key, item.Value);
            }
        }
    }
}
