using Microsoft.AspNetCore.Authorization;
using Riven.Authorization;
using Riven.Identity.Authorization;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using JetBrains.Annotations;

namespace Riven.Authorization
{
    public interface IClaimsChecker
    {
        bool IsGranted([NotNull]string userId, [NotNull]string claim);

        Task<bool> IsGrantedAsync([NotNull]string userId, [NotNull]string claim);

        bool IsGranted([NotNull]string userId, bool requireAllRoleClaims, params string[] claims);

        Task<bool> IsGrantedAsync([NotNull]string userId, bool requireAllRoleClaims, params string[] claims);
    }
}
