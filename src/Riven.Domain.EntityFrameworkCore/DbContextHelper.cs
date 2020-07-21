using JetBrains.Annotations;

using Microsoft.EntityFrameworkCore;

using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Riven
{
    public static class DbContextHelper
    {
        public static string HardDelete { get; } = "HardDelete";

        public static MethodInfo ConfigureGlobalFiltersMethodInfo = typeof(IRivenDbContext).GetMethod(nameof(IRivenDbContext.ConfigureGlobalFilters), BindingFlags.Instance | BindingFlags.Public);

        /// <summary>
        /// 配置查询全局过滤器
        /// </summary>
        /// <param name="modelBuilder"></param>
        /// <param name="dbContext">数据库上下文</param>
        /// <returns></returns>
        public static ModelBuilder ConfigureGlobalFilters([NotNull] this ModelBuilder modelBuilder, [NotNull] DbContext dbContext)
        {
            Check.NotNull(modelBuilder, nameof(modelBuilder));
            Check.NotNull(dbContext, nameof(dbContext));

            if (dbContext is IRivenDbContext rivenDbContext)
            {
                if (rivenDbContext.ServiceProvider == null)
                {
                    return modelBuilder;
                }

                foreach (var entityType in modelBuilder.Model.GetEntityTypes())
                {
                    DbContextHelper.ConfigureGlobalFiltersMethodInfo
                        .MakeGenericMethod(entityType.ClrType)
                        .Invoke(rivenDbContext, new object[] { modelBuilder, entityType });
                }
            }

            return modelBuilder;
        }
    }
}
