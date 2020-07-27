using Riven.Entities.Auditing;
using Riven.MultiTenancy;
using Riven.Reflection;

using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Riven.Entities
{
    public static class EntityHelper
    {
        public static Type SoftDelete { get; }
        public static Type MayHaveTenant { get; }
        public static Type MustHaveTenant { get; }

        public static Type Passivable { get; }

        public static Type Audited { get; }
        public static Type CreationAudited { get; }
        public static Type FullAudited { get; }
        public static Type DeletionAudited { get; }
        public static Type HasCreationTime { get; }
        public static Type HasDeletionTime { get; }
        public static Type HasModificationTime { get; }
        public static Type ModificationAudited { get; }

        static EntityHelper()
        {
            SoftDelete = typeof(ISoftDelete);
            MayHaveTenant = typeof(IMayHaveTenant);
            MustHaveTenant = typeof(IMustHaveTenant);

            Passivable = typeof(IPassivable);

            Audited = typeof(IAudited);
            CreationAudited = typeof(ICreationAudited);
            FullAudited = typeof(IFullAudited);
            DeletionAudited = typeof(IDeletionAudited);
            HasCreationTime = typeof(IHasCreationTime);
            HasDeletionTime = typeof(IHasDeletionTime);
            HasModificationTime = typeof(IHasModificationTime);
            ModificationAudited = typeof(IModificationAudited);
        }


        /// <summary>
        /// 是否为临时对象(不持久化到数据，且没有id)
        /// </summary>
        /// <returns></returns>
        public static bool IsTransient<TKey>(this IEntity<TKey> entity)
        {
            if (EqualityComparer<TKey>.Default.Equals(entity.Id, default(TKey)))
            {
                return true;
            }

            //Workaround for EF Core since it sets int/long to min value when attaching to dbcontext
            if (typeof(TKey) == typeof(int))
            {
                return Convert.ToInt32(entity.Id) <= 0;
            }

            if (typeof(TKey) == typeof(long))
            {
                return Convert.ToInt64(entity.Id) <= 0;
            }

            return false;
        }


        /// <summary>
        /// 实体对比
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static bool EntityEquals<TKey>(IEntity<TKey> entity, object obj)
        {
            if (obj == null || !(obj is Entity<TKey>))
            {
                return false;
            }

            // Same instances must be considered as equal
            if (ReferenceEquals(entity, obj))
            {
                return true;
            }

            // Transient objects are not considered as equal
            var other = (Entity<TKey>)obj;
            if (entity.IsTransient() && other.IsTransient())
            {
                return false;
            }

            // Must have a IS-A relation of types or must be same type
            var typeOfThis = entity.GetType();
            var typeOfOther = other.GetType();
            if (!typeOfThis.GetTypeInfo().IsAssignableFrom(typeOfOther) && !typeOfOther.GetTypeInfo().IsAssignableFrom(typeOfThis))
            {
                return false;
            }

            return entity.Id.Equals(other.Id);
        }


        /// <summary>
        /// 获取硬删除键
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="tenantName"></param>
        /// <returns></returns>
        public static string GetHardDeleteKey(object entity, string tenantName)
        {
            if (MultiTenancyHelper.IsMultiTenantEntity(entity))
            {
                var tenantIdString = string.IsNullOrWhiteSpace(tenantName) ? tenantName : "null";
                return entity.GetType().FullName + ";TenantId=" + tenantIdString + ";Id=" + GetEntityId(entity);
            }

            return entity.GetType().FullName + ";Id=" + GetEntityId(entity);
        }


        /// <summary>
        /// 获取实体Id
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public static object GetEntityId(object entity)
        {
            if (!ReflectionHelper.IsAssignableToGenericType(entity.GetType(), typeof(IEntity<>)))
            {
                throw new Exception(entity.GetType() + " is not an Entity !");
            }

            return ReflectionHelper.GetValueByPath(entity, entity.GetType(), "Id");
        }


        /// <summary>
        /// 是否为软删除的实体
        /// </summary>
        /// <param name="entityType"></param>
        /// <returns></returns>
        public static bool ShouldSoftDeleteEntity(Type entityType)
        {
            if (EntityHelper.SoftDelete.IsAssignableFrom(entityType))
            {
                return true;
            }

            return false;
        }


        /// <summary>
        /// 是否为多租户的实体 - 非必填
        /// </summary>
        /// <param name="entityType"></param>
        /// <returns></returns>
        public static bool ShouldMayHaveTenancyEntity(Type entityType)
        {
            return EntityHelper.MayHaveTenant.IsAssignableFrom(entityType);
        }


        /// <summary>
        /// 是否为多租户的实体 - 必填
        /// </summary>
        /// <param name="entityType"></param>
        /// <returns></returns>
        public static bool ShouldMustHaveTenancyEntity(Type entityType)
        {
            return EntityHelper.MustHaveTenant.IsAssignableFrom(entityType);
        }
    }
}
