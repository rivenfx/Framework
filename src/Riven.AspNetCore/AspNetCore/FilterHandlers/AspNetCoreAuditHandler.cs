using Microsoft.AspNetCore.Mvc.Filters;
using System.Threading.Tasks;

namespace Riven.AspNetCore.FilterHandlers
{
    public class AspNetCoreAuditHandler : IAspNetCoreAuditHandler
    {
        public virtual Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            return next();
        }

        public virtual Task OnPageHandlerExecutionAsync(PageHandlerExecutingContext context, PageHandlerExecutionDelegate next)
        {
            return next();
        }
    }
}
