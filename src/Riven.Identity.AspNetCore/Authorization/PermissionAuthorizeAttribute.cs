using Microsoft.AspNetCore.Authorization;

using System;
using System.Collections.Generic;
using System.Text;

namespace Riven.Authorization
{
    public class PermissionAuthorizeAttribute : AuthorizeAttribute, IPermissionAuthorizeAttribute
    {
        protected string[] _permissions;

        public virtual new string Policy { get; set; }

        public virtual string[] Permissions
        {
            get => _permissions; set
            {
                throw new Exception($"{nameof(PermissionAuthorizeAttribute.Permissions)} XX does not support the 'set'.");
            }
        }

        public virtual bool RequireAll { get; set; }

        public virtual PermissionAuthorizeScope Scope { get; set; }

        public virtual int Sort { get; set; }

        /// <summary>
        /// Creates a new instance of <see cref="PermissionAuthorizeAttribute"/> class.
        /// </summary>
        /// <param name="permissions">A list of permissions to authorize</param>
        public PermissionAuthorizeAttribute(params string[] permissions)
        {
            this.Permissions = permissions;
            this.Scope = PermissionAuthorizeScope.Common;
            this.Policy = PermissionPolicy.POLICY_NAME;
        }
    }

}
