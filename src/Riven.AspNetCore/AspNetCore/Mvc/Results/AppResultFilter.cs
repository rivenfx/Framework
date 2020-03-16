using Microsoft.AspNetCore.Mvc.Filters;
using Riven.AspNetCore.FilterHandlers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Riven.AspNetCore.Mvc.Results
{
    public class AppResultFilter : IResultFilter, IAsyncPageFilter
    {
        protected readonly IAspNetCoreResultHandler _aspNetCoreResultHandler;

        public AppResultFilter(IAspNetCoreResultHandler aspNetCoreResultHandler)
        {
            _aspNetCoreResultHandler = aspNetCoreResultHandler;
        }

        public Task OnPageHandlerExecutionAsync(PageHandlerExecutingContext context, PageHandlerExecutionDelegate next)
        {
            return _aspNetCoreResultHandler.OnPageHandlerExecutionAsync(context, next);
        }

        public Task OnPageHandlerSelectionAsync(PageHandlerSelectedContext context)
        {
            return _aspNetCoreResultHandler.OnPageHandlerSelectionAsync(context);
        }

        public void OnResultExecuted(ResultExecutedContext context)
        {
            _aspNetCoreResultHandler.OnResultExecuted(context);
        }

        public void OnResultExecuting(ResultExecutingContext context)
        {
            _aspNetCoreResultHandler.OnResultExecuting(context);
        }
    }
}
