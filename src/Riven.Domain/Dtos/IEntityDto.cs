using System;
using System.Collections.Generic;
using System.Text;

namespace Riven.Dtos
{
    public interface IEntityDto<TKey>
    {
        /// <summary>
        /// Id
        /// </summary>
        TKey Id { get; set; }
    }

    public interface IEntityDto: IEntityDto<long>
    {

    }
}
