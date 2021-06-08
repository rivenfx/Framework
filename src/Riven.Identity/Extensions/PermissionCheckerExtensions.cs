using Microsoft.Extensions.Localization;
using Riven.Authorization;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Riven.Exceptions;
using System.Net;

namespace Riven.Extensions
{
    public static class PermissionCheckerExtensions
    {
        public static async Task AuthorizeAsync(this IPermissionChecker permissionChecker, IStringLocalizer stringLocalizer, string userId, bool requireAll, params string[] permissions)
        {
            Check.NotNull(permissionChecker, nameof(permissionChecker));
            Check.NotNull(stringLocalizer, nameof(stringLocalizer));


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
            throw new AuthorizationException(errorMessageStringBuilder.ToString())
            {
                Code = (int)HttpStatusCode.Forbidden
            };
        }


        public static async Task AuthorizeAsync(this IPermissionChecker permissionChecker, IStringLocalizer stringLocalizer, bool requireAll, params string[] permissions)
        {
            Check.NotNull(permissionChecker, nameof(permissionChecker));
            Check.NotNull(stringLocalizer, nameof(stringLocalizer));


            if (await permissionChecker.IsGrantedAsync(requireAll, permissions))
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
            throw new AuthorizationException(errorMessageStringBuilder.ToString())
            {
                Code = (int)HttpStatusCode.Forbidden
            };
        }
    }
}
