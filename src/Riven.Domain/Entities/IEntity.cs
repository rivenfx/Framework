using System;
using System.Collections.Generic;
using System.Text;

namespace Riven.Entities
{
    /// <summary>
    /// 实体接口
    /// </summary>
    /// <typeparam name="TKey">主键类型</typeparam>
    public interface IEntity<TKey>
    {
        /// <summary>
        /// Id
        /// </summary>
        TKey Id { get; set; }
    }

    /// <summary>
    /// 实体接口,主键为long
    /// </summary>
    public interface IEntity : IEntity<long>
    {

    }
}
