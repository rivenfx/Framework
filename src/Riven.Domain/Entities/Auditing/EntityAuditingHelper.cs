using Riven.Extensions;
using Riven.MultiTenancy;
using Riven.Timing;
using System;
using System.Collections.Generic;
using System.Text;

namespace Riven.Entities.Auditing
{
    public static class EntityAuditingHelper
    {
        public static void SetCreationAuditProperties(
            object entityAsObj,
            string tenantName,
            string user)
        {
            var entityWithCreationTime = entityAsObj as IHasCreationTime;
            if (entityWithCreationTime == null)
            {
                //Object does not implement IHasCreationTime
                return;
            }

            if (entityWithCreationTime.CreationTime == default(DateTime))
            {
                entityWithCreationTime.CreationTime = Clock.Now;
            }

            if (!(entityAsObj is ICreationAudited))
            {
                //Object does not implement ICreationAudited
                return;
            }

            if (user.IsNullOrWhiteSpace())
            {
                //Unknown user
                return;
            }

            var entity = entityAsObj as ICreationAudited;
            if (entity.Creator != null)
            {
                //CreatorUserId is already set
                return;
            }

            if (MultiTenancyConfig.IsEnabled)
            {
                if (MultiTenancyHelper.IsMultiTenantEntity(entity) &&
                    !MultiTenancyHelper.IsTenantEntity(entity, tenantName))
                {
                    //A tenant entitiy is created by host or a different tenant
                    return;
                }

                if (!tenantName.IsNullOrWhiteSpace() && MultiTenancyHelper.IsHostEntity(entity))
                {
                    //Tenant user created a host entity
                    return;
                }
            }

            //Finally, set Creator!
            entity.Creator = user;
        }

        public static void SetModificationAuditProperties(
            object entityAsObj,
            string tenantName,
            string user)
        {
            if (entityAsObj is IHasModificationTime)
            {
                entityAsObj.As<IHasModificationTime>().LastModificationTime = Clock.Now;
            }

            if (!(entityAsObj is IModificationAudited))
            {
                //Entity does not implement IModificationAudited
                return;
            }

            var entity = entityAsObj.As<IModificationAudited>();

            if (user.IsNullOrWhiteSpace())
            {
                //Unknown user
                entity.LastModifier = null;
                return;
            }

            if (MultiTenancyConfig.IsEnabled)
            {
                if (MultiTenancyHelper.IsMultiTenantEntity(entity) &&
                    !MultiTenancyHelper.IsTenantEntity(entity, tenantName))
                {
                    //A tenant entitiy is modified by host or a different tenant
                    entity.LastModifier = null;
                    return;
                }

                if (!tenantName.IsNullOrWhiteSpace() && MultiTenancyHelper.IsHostEntity(entity))
                {
                    //Tenant user modified a host entity
                    entity.LastModifier = null;
                    return;
                }
            }

            //Finally, set LastModifier!
            entity.LastModifier = user;
        }
    }

}
