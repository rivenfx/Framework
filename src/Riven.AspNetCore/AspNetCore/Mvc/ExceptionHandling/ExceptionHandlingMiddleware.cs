using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Newtonsoft.Json;
using Riven.AspNetCore.Models;
using Riven.Configuration;
using Microsoft.Extensions.Options;

namespace Riven.AspNetCore.Mvc.ExceptionHandling
{
    public class ExceptionHandlingMiddleware : IMiddleware
    {
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            if (context.Request.RouteValues.Count == 0)
            {
                await next(context);
                return;
            }

            var serviceProvider = context.RequestServices;
            var aspNetCoreOptions = serviceProvider.GetRequiredService<IOptions<RivenAspNetCoreOptions>>().Value;

            try
            {
                await next(context);
            }
            catch (Exception ex)
            {
                if (!aspNetCoreOptions.ExceptionHandlingEnable)
                {
                    throw ex;
                }

            }



            if (IsAuthorizationExceptionStatusCode(context))
            {
                var logger = context.RequestServices.GetRequiredService<ILogger<ExceptionHandlingMiddleware>>();

                var exception = new Exception(GetAuthorizationExceptionMessage(context));

                logger.LogError(exception.Message);

                var errorInfo = new ErrorInfo(context.Response.StatusCode, exception.Message, exception.ToString());

                await context.Response.WriteAsync(
                    JsonConvert.SerializeObject(
                        new AjaxResponse<ErrorInfo>()
                        {
                            Success = false,
                            UnAuthorizedRequest = true,
                            Result = errorInfo
                        }
                    )
                );

                aspNetCoreOptions.TriggerHandledException(this, exception);
            }
        }

        protected virtual string GetAuthorizationExceptionMessage(HttpContext context)
        {
            if (context.Response.StatusCode == (int)HttpStatusCode.Forbidden)
            {
                return "DefaultError403";
            }
            return "DefaultError401";
        }

        protected virtual bool IsAuthorizationExceptionStatusCode(HttpContext context)
        {
            return context.Response.StatusCode == (int)HttpStatusCode.Forbidden
                   || context.Response.StatusCode == (int)HttpStatusCode.Unauthorized;
        }
    }
}
