using Microsoft.EntityFrameworkCore.Metadata;

using Riven.Entities;
using Riven.Linq.Expressions;

using System;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Riven.Extensions
{
    public static class ModelBuilderFilterExtenstions
    {
        /// <summary>
        /// 创建软删除过滤器表达式
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <returns></returns>
        public static Expression<Func<TEntity, bool>> CreateSoftDeleteFilterExpression<TEntity>(Expression<Func<TEntity, bool>> expression)
                where TEntity : class
        {
            if (EntityHelper.ShouldSoftDeleteEntity(typeof(TEntity)))
            {
                Expression<Func<TEntity, bool>> softDeleteFilter = e => !((ISoftDelete)e).IsDeleted;
                expression = expression == null ? softDeleteFilter : expression.CombineExpressions(softDeleteFilter);
            }

            return expression;
        }

        /// <summary>
        /// 创建多租户过滤器表达式
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="currentTenantName"></param>
        /// <param name="multiTenancyEnabled"></param>
        /// <returns></returns>
        public static Expression<Func<TEntity, bool>> CreateMultiTenancyFilterExpression<TEntity>(Expression<Func<TEntity, bool>> expression, string currentTenantName)
          where TEntity : class
        {

            if (EntityHelper.ShouldMayHaveTenancyEntity(typeof(TEntity)))
            {
                Expression<Func<TEntity, bool>> mayHaveTenantFilter = e => ((IMayHaveTenant)e).TenantName == currentTenantName;
                expression = expression == null ? mayHaveTenantFilter : expression.CombineExpressions(mayHaveTenantFilter);
            }

            if (EntityHelper.ShouldMustHaveTenancyEntity(typeof(TEntity)))
            {
                Expression<Func<TEntity, bool>> mustHaveTenantFilter = e => ((IMustHaveTenant)e).TenantName == currentTenantName;
                expression = expression == null ? mustHaveTenantFilter : expression.CombineExpressions(mustHaveTenantFilter);
            }

            return expression;
        }
    }
}
