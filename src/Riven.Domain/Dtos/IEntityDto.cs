using System;
using System.Collections.Generic;
using System.Text;

namespace Riven.Dtos
{
    public interface IEntityDto<TPrimaryKey>
    {
        /// <summary>
        /// Id
        /// </summary>
        TPrimaryKey Id { get; set; }
    }

    public interface IEntityDto: IEntityDto<long>
    {

    }
}
