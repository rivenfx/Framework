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
        Task<IList<Claim>> GetPermissionsByUserIdAsync([NotNull]string userId);


        /// <summary>
        /// 根据 用户名 获取用户拥有的 permissions
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        Task<IList<Claim>> GetPermissionsByUserNameAsync([NotNull]string userName);

        /// <summary>
        /// 根据 用户id 获取用户拥有的 role
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        Task<IList<string>> GetRolesByUserIdAsync([NotNull]string userId);

        /// <summary>
        /// 根据 用户名 获取用户拥有的 role
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        Task<IList<string>> GetRolesByUserNameAsync([NotNull]string userName);
    }
}
