using Microsoft.Extensions.Localization;
using Riven.Authorization;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Riven.Exceptions;

namespace Riven.Extensions
{
    public static class PermissionCheckerExtensions
    {
        public static async Task AuthorizeAsync(this IPermissionChecker permissionChecker, IStringLocalizer stringLocalizer, string userId, bool requireAll, params string[] permissions)
        {
            if (await permissionChecker.IsGrantedAsync(userId, requireAll, permissions))
            {
                return;
            }
            var errorMessageStringBuilder = new StringBuilder();
            // TODO: 本地化 permission 名称
            if (requireAll)
            {
                errorMessageStringBuilder.AppendLine(stringLocalizer["SomePermissionDoNotExist"]);
            }
            else
            {
                errorMessageStringBuilder.AppendLine(stringLocalizer["WithoutAnyPermission"]);
            }

            foreach (var permission in permissions)
            {
                errorMessageStringBuilder.AppendLine(stringLocalizer[permission]);
            }
            throw new AuthorizationException(errorMessageStringBuilder.ToString());
        }
    }
}
