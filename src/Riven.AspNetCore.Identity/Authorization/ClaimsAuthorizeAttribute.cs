using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Text;

namespace Riven.Identity.Authorization
{
    public class ClaimsAuthorizeAttribute : AuthorizeAttribute
    {
        public virtual new string Policy { get; protected set; }

        /// <summary>
        /// A list of Claims to authorize.
        /// </summary>
        public string[] Claims { get; }

        /// <summary>
        /// If this property is set to true, all of the <see cref="Claims"/> must be granted.
        /// If it's false, at least one of the <see cref="Claims"/> must be granted.
        /// Default: false.
        /// </summary>
        public bool RequireAll { get; set; }

        /// <summary>
        /// Creates a new instance of <see cref="ClaimsAuthorizeAttribute"/> class.
        /// </summary>
        /// <param name="roleClaims">A list of permissions to authorize</param>
        public ClaimsAuthorizeAttribute(params string[] roleClaims)
        {
            Claims = roleClaims;

            Policy = ClaimsAuthorizationPolicyProvider.POLICY_PREFIX;
        }
    }
}
