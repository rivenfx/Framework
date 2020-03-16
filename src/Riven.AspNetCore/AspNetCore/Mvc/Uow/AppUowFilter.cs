using Microsoft.AspNetCore.Mvc.Filters;
using Riven.AspNetCore.FilterHandlers;
using Riven.AspNetCore.Mvc.Extensions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Riven.AspNetCore.Mvc.Uow
{
    public class AppUowFilter : IAsyncActionFilter, IAsyncPageFilter
    {
        private readonly IAspNetCoreUnitOfWorkHandler _aspNetCoreUnitOfWorkHandler;

        public AppUowFilter(IAspNetCoreUnitOfWorkHandler aspNetCoreUnitOfWorkHandler)
        {
            _aspNetCoreUnitOfWorkHandler = aspNetCoreUnitOfWorkHandler;
        }

        public Task OnPageHandlerSelectionAsync(PageHandlerSelectedContext context)
        {
            return Task.CompletedTask;
        }


        public Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            return this._aspNetCoreUnitOfWorkHandler.UowActionFilterAsync(context, next);
        }

        public Task OnPageHandlerExecutionAsync(PageHandlerExecutingContext context, PageHandlerExecutionDelegate next)
        {
            return _aspNetCoreUnitOfWorkHandler.UowOnPageHandlerExecutionAsync(context, next);
        }
    }
}
