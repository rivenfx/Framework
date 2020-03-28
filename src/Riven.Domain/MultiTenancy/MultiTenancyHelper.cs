using Riven.Entities;
using Riven.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Riven.MultiTenancy
{
    public static class MultiTenancyHelper
    {
        public static bool IsMultiTenantEntity(object entity)
        {
            return entity is IMayHaveTenant || entity is IMustHaveTenant;
        }

        /// <param name="entity">The entity to check</param>
        /// <param name="expectedTenantName">TenantId or null for host</param>
        public static bool IsTenantEntity(object entity, string expectedTenantName)
        {
            return (entity is IMayHaveTenant && entity.As<IMayHaveTenant>().TenantName == expectedTenantName) ||
                   (entity is IMustHaveTenant && entity.As<IMustHaveTenant>().TenantName == expectedTenantName);
        }

        public static bool IsHostEntity(object entity)
        {
            MultiTenancySideAttribute attribute = entity.GetType().GetTypeInfo()
                .GetCustomAttributes(typeof(MultiTenancySideAttribute), true)
                .Cast<MultiTenancySideAttribute>()
                .FirstOrDefault();

            if (attribute == null)
            {
                return false;
            }

            return attribute.Side.HasFlag(MultiTenancySides.Host);
        }
    }
}
