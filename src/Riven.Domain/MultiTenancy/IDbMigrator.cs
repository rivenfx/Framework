using System;


namespace Riven.MultiTenancy
{
    public interface IDbMigrator
    {
        void CreateOrMigrateForHost();

        void CreateOrMigrateForTenant(string tenantName);
    }
}