using Microsoft.AspNetCore.Authorization;

using System;
using System.Collections.Generic;
using System.Text;

namespace Riven.Identity.Authorization
{
    public class PermissionAuthorizeAttribute : AuthorizeAttribute
    {
        public virtual new string Policy { get; set; }

        /// <summary>
        /// A list of permissions to authorize.
        /// </summary>
        public virtual string[] Permissions { get; protected set; }

        /// <summary>
        /// If this property is set to true, all of the <see cref="Permissions"/> must be granted.
        /// If it's false, at least one of the <see cref="Permissions"/> must be granted.
        /// Default: false.
        /// </summary>
        public virtual bool RequireAll { get; set; }

        /// <summary>
        /// Permission 归属,不设置默认视作 <see cref="PermissionAuthorizeScope.Common"/>
        /// </summary>
        public virtual PermissionAuthorizeScope Scope { get; set; }

        /// <summary>
        /// 权限排序号,决定在同级别权限显示的先后顺序
        /// </summary>
        public virtual int Sort { get; set; }

        /// <summary>
        /// Creates a new instance of <see cref="PermissionAuthorizeAttribute"/> class.
        /// </summary>
        /// <param name="permissions">A list of permissions to authorize</param>
        public PermissionAuthorizeAttribute(params string[] permissions)
        {
            this.Permissions = permissions;
            this.Scope = PermissionAuthorizeScope.Common;
            this.Policy = PermissionAuthorizationPolicyProvider.POLICY_NAME;
        }
    }


    /// <summary>
    /// permission 定义归属范围枚举
    /// </summary>
    public enum PermissionAuthorizeScope
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
