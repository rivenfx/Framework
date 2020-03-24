using System;
using System.Collections.Generic;
using System.Text;

namespace Riven.Localization
{
    /// <summary>
    /// 当前语言
    /// </summary>
    public interface ICurrentLanguage
    {
        /// <summary>
        /// 当前语言名称
        /// </summary>
        string Name { get;  }
    }
}
