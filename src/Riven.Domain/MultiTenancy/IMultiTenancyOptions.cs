namespace Riven.MultiTenancy
{
    /// <summary>
    /// 多租户配置项
    /// </summary>
    public interface IMultiTenancyOptions
    {
        /// <summary>
        /// 是否启用多租户
        /// </summary>
        bool IsEnabled { get; set; }

        /// <summary>
        /// 租户名称键值
        /// </summary>
        string TenantNameKey { get; set; }
    }

    public class DefaultMultiTenancyOptions : IMultiTenancyOptions
    {
        public virtual bool IsEnabled
        {
            get => MultiTenancyConfig.IsEnabled;
            set => MultiTenancyConfig.IsEnabled = value;
        }

        public virtual string TenantNameKey
        {
            get => MultiTenancyConfig.TenantNameKey;
            set => MultiTenancyConfig.TenantNameKey = value;
        }
    }
}
