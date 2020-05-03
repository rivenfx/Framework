using Riven.Authorization;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Riven.Extensions
{
    public static class ClaimsCheckerExtensions
    {
        public static async Task AuthorizeAsync(this IClaimsChecker claimsChecker, string userId, bool requireAll, params string[] claims)
        {
            if (await claimsChecker.IsGrantedAsync(userId, requireAll, claims))
            {
                return;
            }

            if (requireAll)
            {
                throw new AuthorizationException(
                    string.Format($"requireAll {string.Join(", ", claims)} 中的某些权限不存在")
                );
            }
            else
            {
                throw new AuthorizationException(
                   string.Format($"not requireAll {string.Join(", ", claims)} 中的任何权限")
               );
            }
        }
    }
}
