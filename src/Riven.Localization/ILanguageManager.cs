using System;
using System.Collections.Generic;
using System.Text;

namespace Riven
{
    /// <summary>
    /// 语言管理器
    /// </summary>
    public interface ILanguageManager
    {
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
        /// 添加一个语言
        /// </summary>
        /// <param name="language"></param>
        void Add(LanguageInfo language);

        /// <summary>
        /// 添加一个集合的语言
        /// </summary>
        /// <param name="languages">语言数据集合</param>
        void AddRange(List<LanguageInfo> languages);
        
        /// <summary>
        /// 根据Key删除语言
        /// </summary>
        /// <param name="languageName">语言名称</param>
        void Remove(string languageName);

        /// <summary>
        /// 清空所有语言
        /// </summary>
        void Clear();
    }
}
