using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Riven.Identity.Authorization
{
    public interface IRoleClaimChecker
    {
        bool IsGranted(IReadOnlyList<RoleClaimAttribute> roleClaimAttributes, string userId);

        Task<bool> IsGrantedAsync(IReadOnlyList<RoleClaimAttribute> roleClaimAttributes, string userId);
    }
}
