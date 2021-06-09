using Riven.Security;
using Riven.Extensions;

namespace Riven.Identity
{
    public class CurrentUser : ICurrentUser
    {
        public virtual ICurrentPrincipalAccessor CurrentPrincipalAccessor => _currentPrincipalAccessor;

        public virtual string UserId { get; protected set; }

        public virtual string UserName { get; protected set; }

        readonly ICurrentPrincipalAccessor _currentPrincipalAccessor;

        public CurrentUser(ICurrentPrincipalAccessor currentPrincipalAccessor)
        {
            _currentPrincipalAccessor = currentPrincipalAccessor;
            UserId = _currentPrincipalAccessor.Principal.GetUserId();
            UserName = _currentPrincipalAccessor.Principal.GetUserName();
        }
    }
}
