using Microsoft.AspNetCore.Http;

using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;

namespace Riven.Security
{
    public class AspNetCoreCurrentPrincipalAccessor : ThreadCurrentPrincipalAccessor
    {
        public override ClaimsPrincipal Principal => _httpContextAccessor.HttpContext?.User ?? base.Principal;

        private readonly IHttpContextAccessor _httpContextAccessor;

        public AspNetCoreCurrentPrincipalAccessor(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }
    }


}
