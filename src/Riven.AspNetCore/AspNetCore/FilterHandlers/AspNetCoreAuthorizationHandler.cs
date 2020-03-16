using Microsoft.AspNetCore.Mvc.Filters;
using System.Threading.Tasks;

namespace Riven.AspNetCore.FilterHandlers
{
    public class AspNetCoreAuthorizationHandler : IAspNetCoreAuthorizationHandler
    {

        public virtual Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {
            return Task.CompletedTask;
        }
    }
}
