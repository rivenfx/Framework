using System.Security.Claims;
using System.Threading;

namespace Riven.Security
{
    public class ThreadCurrentPrincipalAccessor : ICurrentPrincipalAccessor
    {
        public virtual ClaimsPrincipal Principal => Thread.CurrentPrincipal as ClaimsPrincipal;
    }
}
