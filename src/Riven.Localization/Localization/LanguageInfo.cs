using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using System.Text;

namespace Riven.Localization
{
    /// <summary>
    /// 语言信息
    /// </summary>
    public class LanguageInfo
    {
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 语言显示名称
        /// </summary>
        public string DisplayName { get; set; }

        /// <summary>
        /// 图标
        /// </summary>
        public string Icon { get; set; }

        /// <summary>
        /// 是否启用
        /// </summary>
        public bool Enabled { get; set; }

        /// <summary>
        /// 扩展信息
        /// </summary>
        public string Extra { get; set; }

        /// <summary>
        /// 本地化的数据
        /// </summary>
        public Dictionary<string, string> Data { get; protected set; }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="name">语言名称</param>
        /// <param name="displayName">语言显示名称</param>
        /// <param name="icon">显示图标</param>
        /// <param name="enabled">是否启用,默认为true</param>
        /// <param name="extra">扩展信息</param>
        public LanguageInfo(
            [NotNull]string name,
            [NotNull]string displayName,
            string icon = null,
            bool enabled = true,
            string extra = null)
        {
            this.Name = name;
            this.DisplayName = displayName;
            this.Icon = icon;
            this.Enabled = enabled;
            this.Extra = extra;

            this.Data = new Dictionary<string, string>();
        }
    }
}
