using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Riven.Localization
{
    /// <summary>
    /// 语言管理器
    /// </summary>
    public interface ILanguageManager
    {
        /// <summary>
        /// 获取默认语言
        /// </summary>
        /// <returns></returns>
        Task<LanguageInfo> GetDefaultLanguageAsync();

        /// <summary>
        /// 获取默认语言
        /// </summary>
        /// <returns></returns>
        LanguageInfo GetDefaultLanguage();

        /// <summary>
        /// 获取所有语言信息
        /// </summary>
        /// <returns>语言信息集合</returns>
        IReadOnlyList<LanguageInfo> GetAllLanguages();

        /// <summary>
        /// 获取所有激活的语言信息
        /// </summary>
        /// <returns>语言信息集合</returns>
        IReadOnlyList<LanguageInfo> GetEnabledLanguages();

        /// <summary>
        /// 添加或更新一个语言
        /// </summary>
        /// <param name="language"></param>
        void AddOrUpdate(LanguageInfo language);

        /// <summary>
        /// 添加或更新一个集合的语言
        /// </summary>
        /// <param name="languages">语言数据集合</param>
        void AddOrUpdateRange(List<LanguageInfo> languages);

        /// <summary>
        /// 根据Key删除语言
        /// </summary>
        /// <param name="culture">culture</param>
        void Remove(string culture);

        /// <summary>
        /// 设置默认语言
        /// </summary>
        /// <param name="culture">culture</param>
        void ChangeDefaultLanguage(string culture);

        /// <summary>
        /// 清空所有语言
        /// </summary>
        void Clear();
    }
}
