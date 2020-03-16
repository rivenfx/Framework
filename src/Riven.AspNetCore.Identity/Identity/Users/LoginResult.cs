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

        public LoginResult(LoginResultType result, TUser user, ClaimsIdentity identity)
        {
            this.Result = result;
            this.User = user;
            this.Identity = identity;
        }
    }

    public class LoginResult : LoginResult<AppUser>
    {
        public LoginResult(LoginResultType result, AppUser user, ClaimsIdentity identity)
            : base(result,user, identity)
        {
        }
    }
}
