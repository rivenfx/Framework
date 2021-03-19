using Microsoft.AspNetCore.Identity;

using Riven.Identity;

using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;

namespace Riven.Extensions
{
    public static class RivenClaimsPrincipalExtensions
    {
        public static string GetUserId(this ClaimsPrincipal claimsPrincipal)
        {
            if (claimsPrincipal == null)
            {
                return null;
            }

            var userIdString = claimsPrincipal.FindFirstValue(IdentityInfo.IdentityOptions.ClaimsIdentity.UserIdClaimType);
            if (!userIdString.IsNullOrWhiteSpace())
            {
                return userIdString;
            }
            return claimsPrincipal.FindFirstValue(ClaimTypes.NameIdentifier);
        }

        public static string GetUserName(this ClaimsPrincipal claimsPrincipal)
        {
            if (claimsPrincipal == null)
            {
                return null;
            }

            return claimsPrincipal.FindFirstValue(IdentityInfo.IdentityOptions.ClaimsIdentity.UserNameClaimType);
        }
    }
}
