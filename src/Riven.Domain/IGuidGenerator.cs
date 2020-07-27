using System;
using System.Collections.Generic;
using System.Text;

namespace Riven
{
    /// <summary>
    /// GUID生成器接口
    /// </summary>
    public interface IGuidGenerator
    {
        /// <summary>
        /// 创建一个GUID
        /// </summary>
        Guid Create();
    }
}
