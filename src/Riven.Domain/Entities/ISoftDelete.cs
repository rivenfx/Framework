using System;
using System.Collections.Generic;
using System.Text;

namespace Riven.Entities
{
    /// <summary>
    /// 软删除标记接口
    /// </summary>
    public interface ISoftDelete
    {
        /// <summary>
        /// 已删除
        /// </summary>
        bool IsDeleted { get; set; }
    }
}
