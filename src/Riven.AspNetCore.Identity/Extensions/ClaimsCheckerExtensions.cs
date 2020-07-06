using Microsoft.Extensions.Localization;
using Riven.Authorization;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Riven.Exceptions;

namespace Riven.Extensions
{
    public static class ClaimsCheckerExtensions
    {
        public static async Task AuthorizeAsync(this IClaimsChecker claimsChecker, IStringLocalizer stringLocalizer, string userId, bool requireAll, params string[] claims)
        {
            if (await claimsChecker.IsGrantedAsync(userId, requireAll, claims))
            {
                return;
            }
            var errorMessageStringBuilder = new StringBuilder();
            // TODO: 本地化claim名称
            if (requireAll)
            {
                errorMessageStringBuilder.AppendLine(stringLocalizer["SomeClaimDoNotExist"]);
            }
            else
            {
                errorMessageStringBuilder.AppendLine(stringLocalizer["WithoutAnyClaim"]);
            }

            foreach (var claim in claims)
            {
                errorMessageStringBuilder.AppendLine(stringLocalizer[claim]);
            }
            throw new AuthorizationException(errorMessageStringBuilder.ToString());
        }
    }
}
