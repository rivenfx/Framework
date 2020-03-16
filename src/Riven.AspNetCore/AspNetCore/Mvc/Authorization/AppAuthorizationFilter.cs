using Microsoft.AspNetCore.Mvc.Filters;
using Riven.AspNetCore.FilterHandlers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Riven.AspNetCore.Mvc.Authorization
{
    public class AppAuthorizationFilter : IAsyncAuthorizationFilter
    {
        protected readonly IAspNetCoreAuthorizationHandler _aspNetCoreAuthorizationHandler;

        public AppAuthorizationFilter(IAspNetCoreAuthorizationHandler aspNetCoreAuthorizationHandler)
        {
            _aspNetCoreAuthorizationHandler = aspNetCoreAuthorizationHandler;
        }

        public Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {
            return _aspNetCoreAuthorizationHandler.OnAuthorizationAsync(context);
        }
    }
}
