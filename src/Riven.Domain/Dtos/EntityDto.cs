using System;
using System.Collections.Generic;
using System.Text;

namespace Riven.Dtos
{

    [Serializable]
    public class EntityDto : EntityDto<long>
    {

    }

    [Serializable]
    public class EntityDto<TPrimaryKey> : IEntityDto<TPrimaryKey>
    {
        public virtual TPrimaryKey Id { get; set; }
    }
}
