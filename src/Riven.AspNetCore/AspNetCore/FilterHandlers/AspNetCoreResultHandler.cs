using Microsoft.AspNetCore.Mvc.Filters;
using System.Threading.Tasks;

namespace Riven.AspNetCore.FilterHandlers
{
    public class AspNetCoreResultHandler : IAspNetCoreResultHandler
    {
        public virtual Task OnPageHandlerExecutionAsync(PageHandlerExecutingContext context, PageHandlerExecutionDelegate next)
        {
            return next();
        }

        public virtual Task OnPageHandlerSelectionAsync(PageHandlerSelectedContext context)
        {
            return Task.CompletedTask;
        }

        public virtual void OnResultExecuted(ResultExecutedContext context)
        {

        }

        public virtual void OnResultExecuting(ResultExecutingContext context)
        {

        }
    }
}
