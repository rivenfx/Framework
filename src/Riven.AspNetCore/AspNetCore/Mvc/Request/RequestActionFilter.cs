using Microsoft.AspNetCore.Mvc.Filters;
using Riven.AspNetCore.Mvc.Extensions;
using Riven.AspNetCore.Mvc.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Options;
using Riven.Configuration;

namespace Riven.AspNetCore.Mvc.Request
{
    public class RequestActionFilter : IAsyncActionFilter
    {
        public Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var aspNetCoreOptions = context.HttpContext.RequestServices.GetRequiredService<IOptions<RivenAspNetCoreOptions>>().Value;

            context.HttpContext.Items[aspNetCoreOptions.RequestActionInfoName] = new RequestActionInfo()
            {
                IsObjectResult = ActionResultHelper.IsObjectResult(context.ActionDescriptor.GetMethodInfo().ReturnType)
            };

            return next();
        }
    }
}
