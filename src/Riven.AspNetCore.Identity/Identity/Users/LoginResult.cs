using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;

namespace Riven.Identity.Users
{
    public class LoginResult<TUser>
    {
        public LoginResultType Result { get; protected set; }

        public TUser User { get; protected set; }

        public ClaimsIdentity Identity { get; protected set; }

        public ClaimsPrincipal ClaimsPrincipal { get; protected set; }

        public LoginResult(LoginResultType result, TUser user, ClaimsPrincipal claimsPrincipal)
        {
            this.Result = result;
            this.User = user;
            this.ClaimsPrincipal = claimsPrincipal;
            if (claimsPrincipal != null)
            {
                this.Identity = claimsPrincipal.Identity as ClaimsIdentity;
            }
        }
    }

    public class LoginResult : LoginResult<AppUser>
    {
        public LoginResult(LoginResultType result, AppUser user, ClaimsPrincipal claimsPrincipal)
            : base(result, user, claimsPrincipal)
        {
        }
    }
}
