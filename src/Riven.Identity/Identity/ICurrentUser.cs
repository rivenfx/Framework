using Riven.Security;

using System;
using System.Collections.Generic;
using System.Text;

namespace Riven.Identity
{
    /// <summary>
    /// 当前用户
    /// </summary>
    public interface ICurrentUser
    {
        /// <summary>
        /// 当前用户信息查找器
        /// </summary>
        ICurrentPrincipalAccessor CurrentPrincipalAccessor { get; }

        /// <summary>
        /// 用户id
        /// </summary>
        string UserId { get; }

        /// <summary>
        /// 用户名
        /// </summary>
        string UserName { get; }
    }
}
