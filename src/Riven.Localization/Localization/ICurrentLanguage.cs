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
        /// 获取当前语言信息
        /// </summary>
        /// <returns></returns>
        LanguageInfo GetCurrentLanguage();
    }
}
