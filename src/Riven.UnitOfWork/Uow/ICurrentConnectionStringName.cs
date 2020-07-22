using System;
using System.Collections.Generic;
using System.Text;

namespace Riven.Uow
{
    /// <summary>
    /// 当前连接字符串名称获取器
    /// </summary>
    public interface ICurrentConnectionStringName
    {
        /// <summary>
        /// 当前
        /// </summary>
        string Current { get; }
    }
}
