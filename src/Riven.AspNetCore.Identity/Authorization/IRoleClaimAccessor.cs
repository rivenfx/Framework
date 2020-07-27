using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Riven.Authorization
{
    public interface IRoleClaimAccessor
    {
        /// <summary>
        /// 根据角色id获取claims
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        Task<IList<Claim>> GetClaimsByRoleIdAsync([NotNull]string roleId);

        /// <summary>
        /// 根据角色名称获取claims
        /// </summary>
        /// <param name="roleName"></param>
        /// <returns></returns>
        Task<IList<Claim>> GetClaimsByRoleNameAsync([NotNull]string roleName);


        /// <summary>
        /// 根据角色名称获取claims
        /// </summary>
        /// <param name="roleNames"></param>
        /// <returns></returns>
        Task<IList<Claim>> GetClaimsByRoleNamesAsync([NotNull]string[] roleNames);
    }
}
