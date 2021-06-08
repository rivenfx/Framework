using Microsoft.AspNetCore.Mvc.Filters;
using Riven.AspNetCore.Models;
using Riven.AspNetCore.Mvc.Extensions;
using Riven.Reflection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Riven.AspNetCore.Mvc.Results.Wrapping;
using Microsoft.Extensions.Options;
using Riven.Configuration;

namespace Riven.AspNetCore.Mvc.Results
{
    public class RequestResultFilter : IResultFilter, IAsyncPageFilter
    {


        #region IResultFilter

        public void OnResultExecuting(ResultExecutingContext context)
        {
            if (!context.ActionDescriptor.IsControllerAction())
            {
                return;
            }

            var methodInfo = context.ActionDescriptor.GetMethodInfo();

            var wrapResultAttribute = ReflectionHelper
               .GetSingleAttributeOfMemberOrDeclaringTypeOrDefault<WrapResultAttribute>(methodInfo);

            if (wrapResultAttribute == null)
            {
                wrapResultAttribute = context.HttpContext.RequestServices
                    .GetRequiredService<IOptions<RivenAspNetCoreOptions>>()
                    .Value
                    .DefaultWrapResultAttribute;
            }

            if (!wrapResultAttribute.WrapOnSuccess)
            {
                return;
            }

            var requestActionResultWrapperFactory = context.HttpContext.RequestServices
                .GetRequiredService<IRequestActionResultWrapperFactory>();
            requestActionResultWrapperFactory.CreateFor(context).Wrap(context);
        }

        public void OnResultExecuted(ResultExecutedContext context)
        {

        }

        #endregion



        #region IAsyncPageFilter


        public async Task OnPageHandlerExecutionAsync(PageHandlerExecutingContext context, PageHandlerExecutionDelegate next)
        {
            if (context.HandlerMethod == null)
            {
                await next();
                return;
            }

            var pageHandlerExecutedContext = await next();

            var methodInfo = context.HandlerMethod.MethodInfo;

            var wrapResultAttribute = ReflectionHelper
                 .GetSingleAttributeOfMemberOrDeclaringTypeOrDefault<WrapResultAttribute>(methodInfo);

            if (!wrapResultAttribute.WrapOnSuccess)
            {
                return;
            }

            var requestActionResultWrapperFactory = context.HttpContext.RequestServices.GetService<IRequestActionResultWrapperFactory>();
            requestActionResultWrapperFactory.CreateFor(pageHandlerExecutedContext).Wrap(pageHandlerExecutedContext);
        }

        public Task OnPageHandlerSelectionAsync(PageHandlerSelectedContext context)
        {
            return Task.CompletedTask;
        }

        #endregion


    }
}
