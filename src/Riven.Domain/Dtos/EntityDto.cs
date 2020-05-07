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
    public class EntityDto<TKey> : IEntityDto<TKey>
    {
        public virtual TKey Id { get; set; }
    }
}
