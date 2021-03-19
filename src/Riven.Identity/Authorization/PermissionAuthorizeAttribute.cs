using System;
using System.Collections.Generic;
using System.Text;

namespace Riven.Authorization
{
    public interface IPermissionAuthorizeAttribute
    {
        /// <summary>
        /// 权限名称
        /// </summary>
        string[] Permissions { get; set; }

        /// <summary>
        /// 是否校验所有权限
        /// </summary>
        bool RequireAll { get; set; }

        /// <summary>
        /// Permission 归属,不设置默认视作 <see cref="PermissionAuthorizeScope.Common"/>
        /// </summary>
        PermissionAuthorizeScope Scope { get; set; }

        /// <summary>
        /// 权限排序号,决定在同级别权限显示的先后顺序
        /// </summary>
        int Sort { get; set; }
    }
}
