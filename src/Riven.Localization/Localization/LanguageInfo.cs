using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Riven.Localization
{
    /// <summary>
    /// 语言信息
    /// </summary>
    public class LanguageInfo
    {
        private Dictionary<string, string> _texts;

        /// <summary>
        /// Culture
        /// </summary>
        public string Culture { get; set; }

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
        public Dictionary<string, string> Texts
        {
            get => this._texts;
            set
            {
                if (value == null)
                {
                    this._texts.Clear();
                }
                else if (this._texts == null)
                {
                    this._texts = value;
                }
                else
                {
                    foreach (var item in value)
                    {
                        this._texts[item.Key] = item.Value;
                    }
                }
            }
        }
    }
}
