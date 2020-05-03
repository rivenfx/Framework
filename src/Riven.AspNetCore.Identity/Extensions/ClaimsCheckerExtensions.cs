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

            //var localizedPermissionNames = LocalizePermissionNames(permissionChecker, permissionNames);

            //if (requireAll)
            //{
            //    throw new AbpAuthorizationException(
            //        string.Format(
            //            L(
            //                permissionChecker,
            //                "AllOfThesePermissionsMustBeGranted",
            //                "Required permissions are not granted. All of these permissions must be granted: {0}"
            //            ),
            //            string.Join(", ", localizedPermissionNames)
            //        )
            //    );
            //}
            //else
            //{
            //    throw new AbpAuthorizationException(
            //        string.Format(
            //            L(
            //                permissionChecker,
            //                "AtLeastOneOfThesePermissionsMustBeGranted",
            //                "Required permissions are not granted. At least one of these permissions must be granted: {0}"
            //            ),
            //            string.Join(", ", localizedPermissionNames)
            //        )
            //    );
            //}
        }
    }
}
