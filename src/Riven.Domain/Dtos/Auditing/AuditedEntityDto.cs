using Riven.Entities.Auditing;
using System;

namespace Riven.Dtos.Auditing
{
    /// <summary>
    /// A shortcut of <see cref="AuditedEntity{TPrimaryKey}"/> for most used primary key type (<see cref="long"/>).
    /// </summary>
    [Serializable]
    public abstract class AuditedEntityDto : AuditedEntityDto<long>, IEntityDto
    {

    }

    /// <summary>
    /// This class can be used to simplify implementing <see cref="IAudited"/>.
    /// </summary>
    /// <typeparam name="TPrimaryKey">Type of the primary key of the entity</typeparam>
    [Serializable]
    public abstract class AuditedEntityDto<TPrimaryKey> : CreationAuditedEntityDto<TPrimaryKey>, IAudited
    {
        /// <summary>
        /// Last modification date of this entity.
        /// </summary>
        public virtual DateTime? LastModificationTime { get; set; }

        /// <summary>
        /// Last modifier of this entity.
        /// </summary>
        public virtual string LastModifier { get; set; }
    }
}
