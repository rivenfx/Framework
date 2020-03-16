using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Threading.Tasks;

namespace Riven.AspNetCore.FilterHandlers
{
    public class AspNetCoreExceptionHandeler : IAspNetCoreExceptionHandeler
    {
        public virtual void OnException(ExceptionContext context)
        {

        }

        public virtual Task OnExceptionPageHandlerExecutionAsync(PageHandlerExecutingContext context, PageHandlerExecutionDelegate next)
        {
            return next();
        }
    }
}
