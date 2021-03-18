using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Riven.Threading;
using System.Collections.Immutable;
using Riven.Extensions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Http;
using Riven.Identity.Permissions;
using Riven.Identity.Users;

namespace Riven.Authorization
{
    public class DefaultPermissionChecker : IPermissionChecker
    {
        protected readonly IAspNetCoreSession _aspNetCoreSession;
        protected readonly IIdentityUserRoleFinder _userRoleFinder;
        protected readonly IIdentityPermissionFinder _permissionFinder;




        public bool IsGranted([NotNull] string userId, [NotNull] string permission)
        {
            if (permission.IsNullOrWhiteSpace())
            {
                return true;
            }

            Check.NotNullOrWhiteSpace(userId, nameof(userId));


            return this.IsGranted(userId, false, permission);
        }


        public async Task<bool> IsGrantedAsync(string userId, string permission)
        {
            if (permission.IsNullOrWhiteSpace())
            {
                return true;
            }
            Check.NotNullOrWhiteSpace(userId, nameof(userId));


            return await this.IsGrantedAsync(userId, false, permission);
        }



        public bool IsGranted(bool requireAll, params string[] permissions)
        {
            return this.IsGranted(this._aspNetCoreSession.UserId, requireAll, permissions);
        }

        public Task<bool> IsGrantedAsync(bool requireAll, params string[] permissions)
        {
            return this.IsGrantedAsync(this._aspNetCoreSession.UserId, requireAll, permissions);
        }

        public bool IsGranted([NotNull] string permission)
        {
            return this.IsGranted(this._aspNetCoreSession.UserId, permission);
        }

        public Task<bool> IsGrantedAsync([NotNull] string permission)
        {
            return this.IsGrantedAsync(this._aspNetCoreSession.UserId, permission);
        }

        public bool IsGranted([NotNull] string userId, bool requireAll, params string[] permissions)
        {
            if (permissions.IsNullOrEmpty())
            {
                return true;
            }
            Check.NotNullOrWhiteSpace(userId, nameof(userId));



            return AsyncHelper.RunSync(() =>
            {
                return this.IsGrantedAsync(userId, requireAll, permissions);
            });
        }

        public async Task<bool> IsGrantedAsync([NotNull] string userId, bool requireAll, params string[] permissions)
        {
            if (permissions.IsNullOrEmpty())
            {
                return true;
            }
            Check.NotNullOrWhiteSpace(userId, nameof(userId));

            // 去重
            var permissionDistinct = permissions.Distinct();

            // 不需要匹配所有的 Permission
            if (!requireAll)
            {
                // 先校验用户权限
                var userPermissions = await this.GetUserPermissions(userId);
                if (permissionDistinct.Any(o => userPermissions.Contains(o)))
                {
                    return true;
                }

                // 校验角色权限
                var userRolePermissions = await this.GetRolesPermissions(userId);
                if (permissionDistinct.Any(o => userRolePermissions.Contains(o)))
                {
                    return true;
                }

                return false;
            }
            else
            {
                // 用户权限
                var userPermissions = await this.GetUserPermissions(userId);
                // 角色权限
                var userRolePermissions = await this.GetRolesPermissions(userId);

                // 1. userPermissions 与 rolePermissions 取并集,
                // 2. 与 校验标记中的 Permissions 取交集, 最后获得交集数量
                var intersectPermissionsCount = userPermissions.Union(userRolePermissions)
                    .Intersect(permissionDistinct)
                    .Count();

                // 长度一致为true,长度不一致为false
                return intersectPermissionsCount == permissionDistinct.Count();
            }
        }


        /// <summary>
        /// 获取用户拥有的权限
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        protected virtual async Task<IEnumerable<string>> GetUserPermissions(string userId)
        {
            return await this._permissionFinder.FindPermissions(PermissionType.User, userId);
        }

        /// <summary>
        /// 获取用户拥有的角色的权限
        /// </summary>
        /// <param name="roles"></param>
        /// <returns></returns>
        protected virtual async Task<IEnumerable<string>> GetRolesPermissions(string userId)
        {
            var roles = await this._userRoleFinder.GetRolesByUserIdAsync(userId);
            return await this._permissionFinder.FindPermissions(PermissionType.Role, roles.ToArray());
        }
    }
}
