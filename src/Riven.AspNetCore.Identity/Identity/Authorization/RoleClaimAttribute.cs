using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Text;

namespace Riven.Identity.Authorization
{
    public class RoleClaimAttribute : AuthorizeAttribute
    {
        public virtual new string Policy { get; protected set; }

        /// <summary>
        /// A list of RoleClaims to authorize.
        /// </summary>
        public string[] RoleClaims { get; }

        /// <summary>
        /// If this property is set to true, all of the <see cref="RoleClaims"/> must be granted.
        /// If it's false, at least one of the <see cref="RoleClaims"/> must be granted.
        /// Default: false.
        /// </summary>
        public bool RequireAllRoleClaims { get; set; }

        /// <summary>
        /// Creates a new instance of <see cref="RoleClaimAttribute"/> class.
        /// </summary>
        /// <param name="roleClaims">A list of permissions to authorize</param>
        public RoleClaimAttribute(params string[] roleClaims)
        {
            RoleClaims = roleClaims;

            Policy = RoleClaimAuthorizationPolicyProvider.POLICY_PREFIX;
        }
    }
}
