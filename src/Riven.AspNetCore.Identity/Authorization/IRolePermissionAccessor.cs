using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Riven.Authorization
{
    public interface IRolePermissionAccessor
    {
        /// <summary>
        /// 根据角色id获取 permissions
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        Task<IEnumerable<string>> GetPermissionsByRoleIdAsync([NotNull]string roleId);

        /// <summary>
        /// 根据角色名称获取 permissions
        /// </summary>
        /// <param name="roleName"></param>
        /// <returns></returns>
        Task<IEnumerable<string>> GetPermissionsByRoleNameAsync([NotNull]string roleName);


        /// <summary>
        /// 根据角色名称获取 permissions
        /// </summary>
        /// <param name="roleNames"></param>
        /// <returns></returns>
        Task<IEnumerable<string>> GetPermissionsByRoleNamesAsync([NotNull]string[] roleNames);
    }
}
