using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Riven.Entities
{
    public static class EntityHelper
    {
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
        public static bool EntityEquals<TKey>(IEntity<TKey> entity,object obj)
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
    }
}
