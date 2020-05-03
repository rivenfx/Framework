using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;

namespace Riven.Extensions
{
    public static class RivenClaimsPrincipalExtensions
    {
        public static string GetUserId(this ClaimsPrincipal claimsPrincipal, IdentityOptions identityOptions)
        {
            return claimsPrincipal.FindFirstValue(identityOptions.ClaimsIdentity.UserIdClaimType);
        }

        public static string GetUserName(this ClaimsPrincipal claimsPrincipal, IdentityOptions identityOptions)
        {
            return claimsPrincipal.FindFirstValue(identityOptions.ClaimsIdentity.UserNameClaimType);
        }
    }
}
