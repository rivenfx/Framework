using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Collections.Concurrent;
using Riven.Storage;
using Microsoft.Extensions.DependencyInjection;

namespace Riven.Uow
{
    /// <summary>
    /// 连接字符串 Storage
    /// </summary>
    public interface IConnectionStringStorage : IAnyStorage<IConnectionStringProvider>
    {

    }

    public class DefaultConnectionStringStore : AnyStorageBase<IConnectionStringProvider>, IConnectionStringStorage
    {
        public DefaultConnectionStringStore(IServiceProvider serviceProvider)
        {
            var connectionStringProviders = serviceProvider.GetServices<IConnectionStringProvider>()
                .ToDictionary(o => o.Name);


            foreach (var item in connectionStringProviders)
            {
                this.AddOrUpdate(item.Key, item.Value);
            }
        }
    }
}
