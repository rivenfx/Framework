using Microsoft.AspNetCore.Mvc.Filters;
using Riven.AspNetCore.FilterHandlers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Riven.AspNetCore.Mvc.Validation
{
    public class AppValidationFilter : IAsyncActionFilter
    {
        protected readonly IAspNetCoreValidationHandler _aspNetCoreValidationHandler;

        public AppValidationFilter(IAspNetCoreValidationHandler aspNetCoreValidationHandler)
        {
            _aspNetCoreValidationHandler = aspNetCoreValidationHandler;
        }

        public Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            return _aspNetCoreValidationHandler.OnActionExecutionAsync(context, next);
        }
    }
}
