using Microsoft.AspNetCore.Mvc.Filters;
using System.Threading.Tasks;

namespace Riven.AspNetCore.FilterHandlers
{
    public class AspNetCoreValidationHandler : IAspNetCoreValidationHandler
    {
        public virtual Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            return next();
        }
    }
}
