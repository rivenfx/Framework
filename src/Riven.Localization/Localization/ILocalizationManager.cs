using System;
using System.Collections.Generic;
using System.Text;

namespace Riven.Localization
{
    /// <summary>
    /// 本地化管理
    /// </summary>
    public interface ILocalizationManager
    {
        /// <summary>
        /// 当前使用的语言
        /// </summary>
        string CurrentLanguage { get; }

        /// <summary>
        /// 语言信息管理器
        /// </summary>
        ILanguageManager LanguageManager { get; }

        /// <summary>
        /// 使用指定语言进行本地化
        /// </summary>
        /// <param name="culture">Culture</param>
        /// <param name="languageKey">文本键值</param>
        /// <param name="args">参数</param>
        /// <returns>本地化结果</returns>
        string L(string culture, string languageKey, params object[] args);

        /// <summary>
        /// 使用当前语言进行本地化
        /// </summary>
        /// <param name="languageKey">文本键值</param>
        /// <param name="args">参数</param>
        /// <returns>本地化结果</returns>
        string L(string languageKey, params object[] args);
    }
}
