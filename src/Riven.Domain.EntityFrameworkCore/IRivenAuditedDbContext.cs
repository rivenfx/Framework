using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata;

using Riven.Entities;
using Riven.Entities.Auditing;
using Riven.Extensions;
using Riven.Timing;

using System;
using System.Collections.Generic;

namespace Riven
{
    /// <summary>
    /// 审计 DbContext 实现
    /// </summary>
    public interface IRivenAuditedDbContext : IRivenDbContext
    {
        /// <summary>
        /// 自动设置租户名称
        /// </summary>
        bool SuppressAutoSetTenantName => true;




        /// <summary>
        /// 添加新实体
        /// </summary>
        /// <param name="entry"></param>
        /// <param name="userId"></param>
        void ApplyConceptsForAddedEntity(EntityEntry entry, string userId)
        {
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
        void ApplyConceptsForModifiedEntity(EntityEntry entry, string userId)
        {
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
        void ApplyConceptsForDeletedEntity(EntityEntry entry, string userId)
        {
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
        void CheckAndSetId(EntityEntry entry)
        {
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
        void CheckAndSetMustHaveTenantNameProperty(object entityAsObj)
        {
            // 未启用多租户
            if (!this.GetMultiTenancyEnabled())
            {
                return;
            }

            // 自动设置租户名称
            if (SuppressAutoSetTenantName)
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

            var currentTenantName = this.GetCurrentTenantNameOrNull();

            if (string.IsNullOrWhiteSpace(currentTenantName))
            {
                throw new Exception("Can not set TenantName to null or empty for IMustHaveTenant entities!");
            }

            entity.TenantName = currentTenantName;
        }

        /// <summary>
        /// 检查或设置租户名称属性 - 非必填
        /// </summary>
        /// <param name="entityAsObj"></param>
        void CheckAndSetMayHaveTenantNameProperty(object entityAsObj)
        {
            if (!this.GetMultiTenancyEnabled())
            {
                return;
            }

            if (SuppressAutoSetTenantName)
            {
                return;
            }

            if (!(entityAsObj is IMayHaveTenant))
            {
                return;
            }

            var entity = entityAsObj.As<IMayHaveTenant>();

            //Don't set if it's already set
            if (string.IsNullOrWhiteSpace(entity.TenantName))
            {
                return;
            }

            entity.TenantName = GetCurrentTenantNameOrNull();
        }

        /// <summary>
        /// 设置创建审计属性
        /// </summary>
        /// <param name="entityAsObj"></param>
        /// <param name="userId"></param>
        void SetCreationAuditProperties(object entityAsObj, string userId)
        {
            EntityAuditingHelper.SetCreationAuditProperties(entityAsObj, this.GetCurrentTenantNameOrNull(), userId);
        }

        /// <summary>
        /// 设置修改审计属性
        /// </summary>
        /// <param name="entityAsObj"></param>
        /// <param name="userId"></param>
        void SetModificationAuditProperties(object entityAsObj, string userId)
        {
            EntityAuditingHelper.SetModificationAuditProperties(entityAsObj, this.GetCurrentTenantNameOrNull(), userId);
        }

        /// <summary>
        /// 软删除
        /// </summary>
        /// <param name="entry"></param>
        void CancelDeletionForSoftDelete(EntityEntry entry)
        {
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
        void SetDeletionAuditProperties(object entityAsObj, string userId)
        {
            var tenantName = this.GetCurrentTenantNameOrNull();

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
        /// 是否是真实删除的实体
        /// </summary>
        /// <param name="entry"></param>
        /// <returns></returns>
        bool IsHardDeleteEntity(EntityEntry entry)
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

            var currentTenantName = GetCurrentTenantNameOrNull();
            var hardDeleteKey = EntityHelper.GetHardDeleteKey(entry.Entity, currentTenantName);
            return objects.Contains(hardDeleteKey);
        }
    }
}
