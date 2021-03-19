using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;

namespace Riven.Security
{
    public interface ICurrentPrincipalAccessor
    {
        ClaimsPrincipal Principal { get; }
    }
}
