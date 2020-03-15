using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Riven.Entities
{
    /// <summary>
    /// 实体基类
    /// </summary>
    /// <typeparam name="TKey">主键类型</typeparam>
    [Serializable]
    public abstract class Entity<TKey> : IEntity<TKey>
    {

        public virtual TKey Id { get; set; }

        /// <summary>
        /// 是否为临时对象(不持久化到数据，且没有id)
        /// </summary>
        /// <returns></returns>
        public virtual bool IsTransient()
        {
            return EntityHelper.IsTransient(this);
        }

        /// <summary>
        /// 实体对比
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public virtual bool EntityEquals(object obj)
        {
            return EntityHelper.EntityEquals(this, obj);
        }

        public override string ToString()
        {
            return $"[{GetType().Name} {Id}]";
        }
    }

    /// <summary>
    /// 实体基类,主键为long
    /// </summary>
    [Serializable]
    public abstract class Entity : Entity<long>
    {

    }
}
