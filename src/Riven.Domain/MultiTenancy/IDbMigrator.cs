using System;
using System.Threading.Tasks;

namespace Riven.MultiTenancy
{
    public interface IDbMigrator
    {
        void CreateOrMigrateForHost();

        void CreateOrMigrateForTenant(string tenantName);


        Task CreateOrMigrateForHostAsync();

        Task CreateOrMigrateForTenantAsync(string tenantName);
    }
}