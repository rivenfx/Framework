using System;
using System.Collections.Generic;
using System.Text;

namespace Riven.MultiTenancy
{
    /// <summary>
    /// 多租户配置
    /// </summary>
    public static class MultiTenancyConfig
    {
        private static bool? _isEnabled = null;

        /// <summary>
        /// 是否启用多租户,只有第一次设置值会生效
        /// (true=启用,false=不启用)
        /// </summary>
        public static bool IsEnabled
        {
            get
            {
                if (_isEnabled.HasValue)
                {
                    return _isEnabled.Value;
                }

                return false;
            }
            set
            {
                if (_isEnabled.HasValue)
                {
                    return;
                }
                _isEnabled = value;
            }
        }
    }
}
