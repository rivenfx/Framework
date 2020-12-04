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

        #region 是否启用

        static bool? _isEnabled = null;

        /// <summary>
        /// 是否启用多租户,默认值为false,只有第一次设置值会生效
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

        #endregion


        #region 多租户键值

        /// <summary>
        /// 默认存储租户名称键值
        /// </summary>
        const string DEFAULT_TENANT_NAME_KEY = "tenantname";

        static string _tenantNameKey = DEFAULT_TENANT_NAME_KEY;

        /// <summary>
        /// 存储租户名称键值 ,默认值为 <see cref="DEFAULT_TENANT_NAME_KEY"/>, 值不能为空, 第一次设置值会生效
        /// </summary>
        public static string TenantNameKey
        {
            get
            {
                return _tenantNameKey;
            }
            set
            {
                Check.NotNullOrWhiteSpace(value, nameof(TenantNameKey));

                if (_tenantNameKey == DEFAULT_TENANT_NAME_KEY)
                {
                    _tenantNameKey = value.ToLower();
                }
            }
        } 

        #endregion
    }
}
