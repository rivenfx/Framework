using Microsoft.AspNetCore.Authorization;

using System;
using System.Collections.Generic;
using System.Text;

namespace Riven.Identity.Authorization
{
    public class ClaimsAuthorizeAttribute : AuthorizeAttribute
    {
        public virtual new string Policy { get; set; }

        /// <summary>
        /// A list of Claims to authorize.
        /// </summary>
        public virtual string[] Claims { get; protected set; }

        /// <summary>
        /// If this property is set to true, all of the <see cref="Claims"/> must be granted.
        /// If it's false, at least one of the <see cref="Claims"/> must be granted.
        /// Default: false.
        /// </summary>
        public virtual bool RequireAll { get; set; }

        /// <summary>
        /// Claims 归属,不设置默认视作 <see cref="ClaimsAuthorizeScope.Common"/>
        /// </summary>
        public virtual ClaimsAuthorizeScope Scope { get; set; }

        /// <summary>
        /// Creates a new instance of <see cref="ClaimsAuthorizeAttribute"/> class.
        /// </summary>
        /// <param name="roleClaims">A list of permissions to authorize</param>
        public ClaimsAuthorizeAttribute(params string[] roleClaims)
        {
            Claims = roleClaims;
            Scope = ClaimsAuthorizeScope.Common;
            Policy = ClaimsAuthorizationPolicyProvider.POLICY_NAME;
        }
    }


    /// <summary>
    /// Claims定义归属范围枚举
    /// </summary>
    public enum ClaimsAuthorizeScope
    {
        /// <summary>
        /// 公共
        /// </summary>
        Common = 0,
        /// <summary>
        /// 宿主
        /// </summary>
        Host = 1
    }
}
