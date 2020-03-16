using System;
using System.Collections.Generic;
using System.Text;

namespace Riven.Identity.Permissions
{
    /// <summary>
    /// 权限
    /// </summary>
    public class AppPermissions
    {
        /// <summary>
        /// 父级权限名称
        /// </summary>
        public string ParentName { get; set; }

        /// <summary>
        /// 权限名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 显示的权限名称
        /// </summary>
        public string DisplayName { get; set; }

        /// <summary>
        /// 描述信息
        /// </summary>
        public string Description { get; set; }
    }
}
