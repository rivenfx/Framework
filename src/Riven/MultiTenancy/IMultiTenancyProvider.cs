using System;
using System.Collections.Generic;
using System.Text;

namespace Riven.MultiTenancy
{
    /// <summary>
    /// 租户提供者
    /// </summary>
    public interface IMultiTenancyProvider
    {
        /// <summary>
        /// 获取当前租户名称或空
        /// </summary>
        /// <returns></returns>
        string CurrentTenantNameOrNull();
    }
}
