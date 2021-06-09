using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;

using Riven.Identity.Roles;
using Riven.Identity.Users;

using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Riven.Identity
{
    public class IdentityUserClaimsPrincipalFactory<TUser, TRole, TKey> : UserClaimsPrincipalFactory<TUser, TRole>
        where TUser : User<TKey>
        where TRole : Role<TKey>
        where TKey : IEquatable<TKey>
    {
        public IdentityUserClaimsPrincipalFactory(
          UserManager<TUser> userManager,
          RoleManager<TRole> roleManager,
          IOptions<IdentityOptions> options
          ) : base(userManager, roleManager, options)
        {
        }

        public override Task<ClaimsPrincipal> CreateAsync(TUser user)
        {
            var claimsPrincipal = base.CreateAsync(user);
            return claimsPrincipal;
        }


        protected override async Task<ClaimsIdentity> GenerateClaimsAsync(TUser user)
        {
            var userId = await UserManager.GetUserIdAsync(user);
            var userName = await UserManager.GetUserNameAsync(user);

            var id = new ClaimsIdentity(
                // REVIEW: Used to match Application scheme
                "Identity.Application", 
                Options.ClaimsIdentity.UserNameClaimType,
                Options.ClaimsIdentity.RoleClaimType
                );

            id.AddClaim(new Claim(Options.ClaimsIdentity.UserIdClaimType, userId));
            id.AddClaim(new Claim(Options.ClaimsIdentity.UserNameClaimType, userName));


            if (UserManager.SupportsUserSecurityStamp)
            {
                id.AddClaim(new Claim(Options.ClaimsIdentity.SecurityStampClaimType,
                    await UserManager.GetSecurityStampAsync(user)));
            }
            return id;
        }
    }
}
