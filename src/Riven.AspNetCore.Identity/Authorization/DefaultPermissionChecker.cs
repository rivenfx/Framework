using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Riven.Threading;
using System.Collections.Immutable;
using Riven.Extensions;

namespace Riven.Authorization
{
    public class DefaultPermissionChecker : IPermissionChecker
    {
        readonly IUserRolePermissionAccessor _userPermissionAccessor;
        readonly IRolePermissionAccessor _rolePermissionAccessor;

        public DefaultPermissionChecker(IUserRolePermissionAccessor userPermissionAccessor, IRolePermissionAccessor rolePermissionAccessor)
        {
            _userPermissionAccessor = userPermissionAccessor;
            _rolePermissionAccessor = rolePermissionAccessor;
        }

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

        public bool IsGranted([NotNull] string userId, bool requireAllRolePermissions, params string[] permissions)
        {
            if (permissions.IsNullOrEmpty())
            {
                return true;
            }
            Check.NotNullOrWhiteSpace(userId, nameof(userId));



            return AsyncHelper.RunSync(() =>
            {
                return this.IsGrantedAsync(userId, requireAllRolePermissions, permissions);
            });
        }

        public async Task<bool> IsGrantedAsync([NotNull] string userId, bool requireAllRolePermissions, params string[] permissions)
        {
            if (permissions.IsNullOrEmpty())
            {
                return true;
            }
            Check.NotNullOrWhiteSpace(userId, nameof(userId));


            // 去重
            var permissionDistinct = permissions.Distinct();

            // 不需要匹配所有的 Permission
            if (!requireAllRolePermissions)
            {
                var userPermissions = (await _userPermissionAccessor.GetPermissionsByUserIdAsync(userId)).Select(o => o.Value);

                if (permissionDistinct.Any(o => userPermissions.Contains(o)))
                {
                    return true;
                }

                var userRoleNames = await _userPermissionAccessor.GetRolesByUserIdAsync(userId);
                var userRolePermissions = (await _rolePermissionAccessor.GetPermissionsByRoleNamesAsync(userRoleNames.ToArray())).Select(o => o.Value);


                if (permissionDistinct.Any(o => userRolePermissions.Contains(o)))
                {
                    return true;
                }


                return false;
            }
            else
            {
                var userPermissions = (await _userPermissionAccessor.GetPermissionsByUserIdAsync(userId)).Select(o => o.Value);

                var userRoleNames = await _userPermissionAccessor.GetRolesByUserIdAsync(userId);
                var userRolePermissions = (await _rolePermissionAccessor.GetPermissionsByRoleNamesAsync(userRoleNames.ToArray())).Select(o => o.Value);

                // userPermissions 与 rolePermissions 取并集, 再与 校验标记中的 Permissions 取交集, 最后获得交集数量
                var intersectPermissionsCount = userPermissions.Union(userRolePermissions).Intersect(permissionDistinct).Count();

                // 长度一致为true,长度不一致为false
                return intersectPermissionsCount == permissionDistinct.Count();
            }
        }
    }
}
