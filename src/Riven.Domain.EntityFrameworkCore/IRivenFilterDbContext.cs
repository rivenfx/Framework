using Microsoft.EntityFrameworkCore;

using Riven.Extensions;

using System;
using System.Linq.Expressions;

namespace Riven
{
    /// <summary>
    /// 过滤器 DbContext 接口和默认实现
    /// </summary>
    public interface IRivenFilterDbContext : IRivenDbContext
    {

        /// <summary>
        /// 配置全局过滤器
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="modelBuilder"></param>
        /// <param name="entityType"></param>
        void ConfigureGlobalFilters<TEntity>(ModelBuilder modelBuilder)
            where TEntity : class
        {
            Expression<Func<TEntity, bool>> filterExpression = null;

            filterExpression = ModelBuilderFilterExtenstions.CreateSoftDeleteFilterExpression(filterExpression);
            filterExpression = ModelBuilderFilterExtenstions.CreateMultiTenancyFilterExpression(
                filterExpression,
                this.GetCurrentTenantNameOrNull(),
                this.GetMultiTenancyEnabled()
            );

            if (filterExpression != null)
            {
                modelBuilder.Entity<TEntity>().HasQueryFilter(filterExpression);
            }
        }
    }
}
