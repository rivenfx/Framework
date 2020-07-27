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
using Riven.AspNetCore.Models;
using Riven.Reflection;
using Riven.Extensions;

namespace Riven.AspNetCore.Mvc.Request
{
    public class RequestActionFilter : IAsyncActionFilter
    {
        public Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var actionMethodInfo = context.ActionDescriptor.GetMethodInfo();

            context.HttpContext.SetRequestActionInfo(new RequestActionInfo()
            {
                IsObjectResult = ActionResultHelper.IsObjectResult(actionMethodInfo.ReturnType),
                WrapResultAttribute = ReflectionHelper.GetSingleAttributeOfMemberOrDeclaringTypeOrDefault<WrapResultAttribute>(actionMethodInfo),
                IsAjax = context.HttpContext.IsAjax()
            });

            return next();
        }
    }
}
