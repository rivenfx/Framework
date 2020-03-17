using Microsoft.AspNetCore.Mvc.Filters;
using Riven.AspNetCore.FilterHandlers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Riven.AspNetCore.Mvc.Auditing
{
    public class AppAuditFilter : IAsyncActionFilter, IAsyncPageFilter
    {
        protected readonly IAspNetCoreAuditHandler _aspNetCoreAuditHandler;

        public AppAuditFilter(IAspNetCoreAuditHandler aspNetCoreAuditHandler)
        {
            _aspNetCoreAuditHandler = aspNetCoreAuditHandler;
        }

        public Task OnPageHandlerSelectionAsync(PageHandlerSelectedContext context)
        {
            return Task.CompletedTask;
        }


        public Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            return _aspNetCoreAuditHandler.OnActionExecutionAsync(context, next);
        }

        public Task OnPageHandlerExecutionAsync(PageHandlerExecutingContext context, PageHandlerExecutionDelegate next)
        {
            return _aspNetCoreAuditHandler.OnPageHandlerExecutionAsync(context, next);
        }

 
    }
}
