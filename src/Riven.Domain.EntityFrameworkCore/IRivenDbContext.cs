using Microsoft.EntityFrameworkCore.Metadata;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System.Collections.Concurrent;
using Microsoft.Extensions.Logging;
using Riven.Uow;
using Riven.Uow.Providers;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using Riven.Extensions;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Riven.Entities;
using Riven.Entities.Auditing;
using Riven.Timing;
using System.Threading;
using System.Threading.Tasks;
using System.Data;
using System.Linq;
using Riven.MultiTenancy;

namespace Riven
{
    /// <summary>
    /// Riven 数据库上下文接口, 包含过滤器和审计
    /// </summary>
    public interface IRivenDbContext
    {
        /// <summary>
        /// 依赖注入容器
        /// </summary>
        IServiceProvider ServiceProvider { get; }

        /// <summary>
        /// 日志
        /// </summary>
        ILogger Logger { get; }

        /// <summary>
        /// Guid 生成器
        /// </summary>
        IGuidGenerator GuidGenerator { get; }

        /// <summary>
        /// 是否审计自动设置租户名称
        /// </summary>
        bool AuditSuppressAutoSetTenantName { get; }


        /// <summary>
        /// 获取当前用户Id
        /// </summary>
        /// <returns></returns>
        string CurrentUserId { get; }

        /// <summary>
        /// 获取当前租户名称
        /// </summary>
        string CurrentTenantName { get; }

        /// <summary>
        /// 当前工作单元提供者
        /// </summary>
        ICurrentUnitOfWorkProvider CurrentUnitOfWorkProvider { get; }


        #region EFCore DbContext 独有方法

        /// <summary>
        /// 将实例转换为EntityEntry
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        EntityEntry ConvertToEntry(object obj)
        {
            return (this as DbContext)?.Entry(obj);
        }

        #endregion


        #region Filter

        /// <summary>
        /// 配置全局过滤器
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="modelBuilder"></param>
        /// <param name="entityType"></param>
        public virtual void ConfigureGlobalFilters<TEntity>(ModelBuilder modelBuilder)
                where TEntity : class
        {
            if (ServiceProvider == null)
            {
                return;
            }

            var filterExpression = default(Expression<Func<TEntity, bool>>);

            // 软删除
            if (EntityHelper.ShouldSoftDeleteEntity(typeof(TEntity)))
            {
                Expression<Func<TEntity, bool>> softDeleteFilter = e => !((ISoftDelete)e).IsDeleted;
                filterExpression = filterExpression == null ? softDeleteFilter : filterExpression.CombineExpressions(softDeleteFilter);
            }

            // 多租户1
            if (EntityHelper.ShouldMayHaveTenancyEntity(typeof(TEntity)))
            {
                Expression<Func<TEntity, bool>> mayHaveTenantFilter = e => ((IMayHaveTenant)e).TenantName == this.CurrentTenantName;
                filterExpression = filterExpression == null ? mayHaveTenantFilter : filterExpression.CombineExpressions(mayHaveTenantFilter);
            }
            // 多租户2
            if (EntityHelper.ShouldMustHaveTenancyEntity(typeof(TEntity)))
            {
                Expression<Func<TEntity, bool>> mustHaveTenantFilter = e => ((IMustHaveTenant)e).TenantName == this.CurrentTenantName;
                filterExpression = filterExpression == null ? mustHaveTenantFilter : filterExpression.CombineExpressions(mustHaveTenantFilter);
            }

            // 将筛选表达式应用到对应的实体类型
            if (filterExpression != null)
            {
                modelBuilder.Entity<TEntity>().HasQueryFilter(filterExpression);
            }
        }

        #endregion


        #region Audited

        /// <summary>
        /// 应用审计
        /// </summary>
        /// <param name="changeTracker"></param>
        public virtual void ApplyAudit(ChangeTracker changeTracker)
        {
            if (ServiceProvider == null || !changeTracker.HasChanges())
            {
                return;
            }

            var userId = this.CurrentUserId;

            foreach (var entry in changeTracker.Entries().ToList())
            {
                if (entry.State != EntityState.Modified && entry.CheckOwnedEntityChange())
                {
                    this.ConvertToEntry(entry.Entity).State = EntityState.Modified;
                }

                switch (entry.State)
                {
                    case EntityState.Added:
                        ApplyAuditForAddedEntity(entry, userId);
                        break;
                    case EntityState.Modified:
                        ApplyAuditForModifiedEntity(entry, userId);
                        break;
                    case EntityState.Deleted:
                        ApplyAuditForDeletedEntity(entry, userId);
                        break;
                }
            }
        }

        /// <summary>
        /// 添加新实体
        /// </summary>
        /// <param name="entry"></param>
        /// <param name="userId"></param>
        public virtual void ApplyAuditForAddedEntity(EntityEntry entry, string userId)
        {
            if (ServiceProvider == null)
            {
                return;
            }

            CheckAndSetId(entry);
            CheckAndSetMustHaveTenantNameProperty(entry.Entity);
            CheckAndSetMayHaveTenantNameProperty(entry.Entity);
            SetCreationAuditProperties(entry.Entity, userId);
        }


        /// <summary>
        /// 修改实体
        /// </summary>
        /// <param name="entry"></param>
        /// <param name="userId"></param>
        public virtual void ApplyAuditForModifiedEntity(EntityEntry entry, string userId)
        {
            if (ServiceProvider == null)
            {
                return;
            }

            SetModificationAuditProperties(entry.Entity, userId);
            if (entry.Entity is ISoftDelete && entry.Entity.As<ISoftDelete>().IsDeleted)
            {
                SetDeletionAuditProperties(entry.Entity, userId);
            }
        }


        /// <summary>
        /// 删除实体
        /// </summary>
        /// <param name="entry"></param>
        /// <param name="userId"></param>
        public virtual void ApplyAuditForDeletedEntity(EntityEntry entry, string userId)
        {
            if (ServiceProvider == null)
            {
                return;
            }

            if (IsHardDeleteEntity(entry))
            {
                return;
            }

            CancelDeletionForSoftDelete(entry);
            SetDeletionAuditProperties(entry.Entity, userId);
        }


        /// <summary>
        /// 检查或设置Id
        /// </summary>
        /// <param name="entry"></param>
        public virtual void CheckAndSetId(EntityEntry entry)
        {
            if (ServiceProvider == null)
            {
                return;
            }

            //Set GUID Ids
            var entity = entry.Entity as IEntity<Guid>;
            if (entity != null && entity.Id == Guid.Empty)
            {
                var idPropertyEntry = entry.Property("Id");

                if (idPropertyEntry != null && idPropertyEntry.Metadata.ValueGenerated == ValueGenerated.Never)
                {
                    entity.Id = GuidGenerator == null ? Guid.NewGuid() : GuidGenerator.Create();
                }
            }
        }


        /// <summary>
        /// 检查或设置租户名称属性 - 必填
        /// </summary>
        /// <param name="entityAsObj"></param>
        public virtual void CheckAndSetMustHaveTenantNameProperty(object entityAsObj)
        {
            if (ServiceProvider == null)
            {
                return;
            }

            // 自动设置租户名称
            if (!AuditSuppressAutoSetTenantName)
            {
                return;
            }

            //Only set IMustHaveTenant entities
            if (!(entityAsObj is IMustHaveTenant))
            {
                return;
            }

            var entity = entityAsObj.As<IMustHaveTenant>();

            //Don't set if it's already set
            if (!string.IsNullOrWhiteSpace(entity.TenantName))
            {
                return;
            }

            var currentTenantName = this.CurrentTenantName;

            if (!string.IsNullOrWhiteSpace(this.CurrentTenantName))
            {
                throw new Exception("Can not set TenantName to null or empty for IMustHaveTenant entities!");
            }

            entity.TenantName = currentTenantName;
        }


        /// <summary>
        /// 检查或设置租户名称属性 - 非必填
        /// </summary>
        /// <param name="entityAsObj"></param>
        public virtual void CheckAndSetMayHaveTenantNameProperty(object entityAsObj)
        {
            if (ServiceProvider == null)
            {
                return;
            }

            if (!AuditSuppressAutoSetTenantName)
            {
                return;
            }

            if (!(entityAsObj is IMayHaveTenant))
            {
                return;
            }

            var entity = entityAsObj.As<IMayHaveTenant>();

            //Don't set if it's already set
            if (!string.IsNullOrWhiteSpace(entity.TenantName))
            {
                return;
            }

            entity.TenantName = this.CurrentTenantName;
        }


        /// <summary>
        /// 设置创建审计属性
        /// </summary>
        /// <param name="entityAsObj"></param>
        /// <param name="userId"></param>
        public virtual void SetCreationAuditProperties(object entityAsObj, string userId)
        {
            if (ServiceProvider == null)
            {
                return;
            }

            EntityAuditingHelper.SetCreationAuditProperties(
                entityAsObj,
                this.CurrentTenantName,
                userId);
        }


        /// <summary>
        /// 设置修改审计属性
        /// </summary>
        /// <param name="entityAsObj"></param>
        /// <param name="userId"></param>
        public virtual void SetModificationAuditProperties(object entityAsObj, string userId)
        {
            if (ServiceProvider == null)
            {
                return;
            }

            EntityAuditingHelper.SetModificationAuditProperties(
                entityAsObj,
                this.CurrentTenantName,
                userId);
        }


        /// <summary>
        /// 软删除
        /// </summary>
        /// <param name="entry"></param>
        public virtual void CancelDeletionForSoftDelete(EntityEntry entry)
        {
            if (ServiceProvider == null)
            {
                return;
            }

            if (!(entry.Entity is ISoftDelete))
            {
                return;
            }

            entry.Reload();
            entry.State = EntityState.Modified;
            entry.Entity.As<ISoftDelete>().IsDeleted = true;
        }


        /// <summary>
        /// 设置删除审计属性
        /// </summary>
        /// <param name="entityAsObj"></param>
        /// <param name="userId"></param>
        public virtual void SetDeletionAuditProperties(object entityAsObj, string userId)
        {
            if (ServiceProvider == null)
            {
                return;
            }

            var tenantName = this.CurrentTenantName;

            if (entityAsObj is IHasDeletionTime)
            {
                var entity = entityAsObj.As<IHasDeletionTime>();

                if (entity.DeletionTime == null)
                {
                    entity.DeletionTime = Clock.Now;
                }
            }

            if (entityAsObj is IDeletionAudited)
            {
                var entity = entityAsObj.As<IDeletionAudited>();

                if (entity.Deleter != null)
                {
                    return;
                }

                if (userId == null)
                {
                    entity.Deleter = null;
                    return;
                }

                //Special check for multi-tenant entities
                if (entity is IMayHaveTenant || entity is IMustHaveTenant)
                {
                    //Sets LastModifierUserId only if current user is in same tenant/host with the given entity
                    if ((entity is IMayHaveTenant && entity.As<IMayHaveTenant>().TenantName == tenantName) ||
                        (entity is IMustHaveTenant && entity.As<IMustHaveTenant>().TenantName == tenantName))
                    {
                        entity.Deleter = userId;
                    }
                    else
                    {
                        entity.Deleter = null;
                    }
                }
                else
                {
                    entity.Deleter = userId;
                }
            }
        }


        /// <summary>
        /// 是否是真删除的实体
        /// </summary>
        /// <param name="entry"></param>
        /// <returns></returns>
        public virtual bool IsHardDeleteEntity(EntityEntry entry)
        {
            if (CurrentUnitOfWorkProvider?.Current?.Items == null)
            {
                return false;
            }

            if (!CurrentUnitOfWorkProvider.Current.Items.ContainsKey(DbContextHelper.HardDelete))
            {
                return false;
            }

            var hardDeleteItems = CurrentUnitOfWorkProvider.Current.Items[DbContextHelper.HardDelete];
            if (!(hardDeleteItems is HashSet<string> objects))
            {
                return false;
            }

            var currentTenantName = this.CurrentTenantName;
            var hardDeleteKey = EntityHelper.GetHardDeleteKey(entry.Entity, currentTenantName);
            return objects.Contains(hardDeleteKey);
        }

        #endregion
    }
}