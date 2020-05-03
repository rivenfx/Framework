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
    public class DefaultRoleClaimChecker : IClaimsChecker
    {
        readonly IUserRoleClaimAccessor _userClaimAccessor;
        readonly IRoleClaimAccessor _roleClaimAccessor;

        public DefaultRoleClaimChecker(IUserRoleClaimAccessor userClaimAccessor, IRoleClaimAccessor roleClaimAccessor)
        {
            _userClaimAccessor = userClaimAccessor;
            _roleClaimAccessor = roleClaimAccessor;
        }

        public bool IsGranted([NotNull] string userId, [NotNull] string claim)
        {
            if (claim.IsNullOrWhiteSpace())
            {
                return true;
            }

            Check.NotNullOrWhiteSpace(userId, nameof(userId));


            return this.IsGranted(userId, false, claim);
        }


        public async Task<bool> IsGrantedAsync(string userId, string claim)
        {
            if (claim.IsNullOrWhiteSpace())
            {
                return true;
            }
            Check.NotNullOrWhiteSpace(userId, nameof(userId));


            return await this.IsGrantedAsync(userId, false, claim);
        }

        public bool IsGranted([NotNull] string userId, bool requireAllRoleClaims, params string[] claims)
        {
            if (claims.IsNullOrEmpty())
            {
                return true;
            }
            Check.NotNullOrWhiteSpace(userId, nameof(userId));



            return AsyncHelper.RunSync(() =>
            {
                return this.IsGrantedAsync(userId, requireAllRoleClaims, claims);
            });
        }

        public async Task<bool> IsGrantedAsync([NotNull] string userId, bool requireAllRoleClaims, params string[] claims)
        {
            if (claims.IsNullOrEmpty())
            {
                return true;
            }
            Check.NotNullOrWhiteSpace(userId, nameof(userId));


            // 去重
            var claimsDistinct = claims.Distinct();

            // 不需要匹配所有的 claim
            if (!requireAllRoleClaims)
            {
                var userClaims = (await _userClaimAccessor.GetClaimsByUserIdAsync(userId)).Select(o => o.Value);

                if (claimsDistinct.Any(o => userClaims.Contains(o)))
                {
                    return true;
                }

                var userRoleNames = await _userClaimAccessor.GetRolesByUserIdAsync(userId);
                var userRoleClaims = (await _roleClaimAccessor.GetClaimsByRoleNamesAsync(userRoleNames.ToArray())).Select(o => o.Value);


                if (claimsDistinct.Any(o => userRoleClaims.Contains(o)))
                {
                    return true;
                }


                return false;
            }
            else
            {
                var userClaims = (await _userClaimAccessor.GetClaimsByUserIdAsync(userId)).Select(o => o.Value);

                var userRoleNames = await _userClaimAccessor.GetRolesByUserIdAsync(userId);
                var userRoleClaims = (await _roleClaimAccessor.GetClaimsByRoleNamesAsync(userRoleNames.ToArray())).Select(o => o.Value);

                // userClaims 与 roleCliams 取并集, 再与 校验标记中的 claims 取交集, 最后获得交集数量
                var intersectClaimsCount = userClaims.Union(userRoleClaims).Intersect(claimsDistinct).Count();

                // 长度一致为true,长度不一致为false
                return intersectClaimsCount == claimsDistinct.Count();
            }
        }
    }
}
