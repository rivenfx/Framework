using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Riven.Authorization
{
    public interface IUserRolePermissionAccessor
    {
        /// <summary>
        /// 根据 用户id 获取用户拥有的 permissions
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        Task<IEnumerable<string>> GetPermissionsByUserIdAsync([NotNull]string userId);


        /// <summary>
        /// 根据 用户名 获取用户拥有的 permissions
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        Task<IEnumerable<string>> GetPermissionsByUserNameAsync([NotNull]string userName);

        /// <summary>
        /// 根据 用户id 获取用户拥有的 role
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        Task<IEnumerable<string>> GetRolesByUserIdAsync([NotNull]string userId);

        /// <summary>
        /// 根据 用户名 获取用户拥有的 role
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        Task<IEnumerable<string>> GetRolesByUserNameAsync([NotNull]string userName);
    }
}
