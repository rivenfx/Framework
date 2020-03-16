using Microsoft.AspNetCore.Mvc.Filters;
using Riven.AspNetCore.Mvc.Extensions;
using System;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace Riven.AspNetCore.FilterHandlers
{
    public class NullAspNetCoreUnitOfWorkHandler : IAspNetCoreUnitOfWorkHandler
    {
        public virtual Task UowActionFilterAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            return next();
        }

        public virtual Task UowOnPageHandlerExecutionAsync(PageHandlerExecutingContext context, PageHandlerExecutionDelegate next)
        {
            return next();
        }
    }
}
