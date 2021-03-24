using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

using Riven.Extensions;
using Riven.Uow;

using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;
using System.Transactions;

namespace Riven.Repositories
{
    public class EfCoreTableNameAccessor : ITableNameAccessor
    {
        static readonly ConcurrentDictionary<string, string> _dict = new ConcurrentDictionary<string, string>();

        public EfCoreTableNameAccessor(IServiceProvider serviceProvider)
        {
            using (var scope = serviceProvider.CreateScope())
            {
                var uowManager = serviceProvider.GetService<IUnitOfWorkManager>();
                using (uowManager.Begin(TransactionScopeOption.Suppress))
                {
                    var dbContext = uowManager.Current.GetDbContext();
                    foreach (var entityType in dbContext.Model.GetEntityTypes())
                    {
                        _dict[entityType.Name] = entityType.GetTableName();
                    }

                }
            }

        }


        public string GetTableName<T>()
        {
            if (_dict.TryGetValue(typeof(T).FullName, out var val))
            {
                return val;
            }

            return null;

        }

        public string GetTableName(Type type)
        {
            Check.NotNull(type, nameof(type));

            if (_dict.TryGetValue(type.FullName, out var val))
            {
                return val;
            }

            return null;
        }
    }
}
