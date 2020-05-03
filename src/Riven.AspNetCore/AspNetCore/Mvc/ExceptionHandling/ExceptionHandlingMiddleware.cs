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
using Riven.AspNetCore.Mvc.Extensions;
using Microsoft.Net.Http.Headers;

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

            var logger = context.RequestServices.GetRequiredService<ILogger<ExceptionHandlingMiddleware>>();
            var aspNetCoreOptions = context.RequestServices.GetRequiredService<IOptions<RivenAspNetCoreOptions>>().Value;


            try
            {
                await next(context);
            }
            catch (Exception ex)
            {
                // 发生异常,但是已经响应了
                if (context.Response.HasStarted)
                {
                    logger.LogWarning("The application has an exception, but is already responding!");
                    throw;
                }

                var requestActionInfo = context.GetRequestActionInfo();

                var wrapResultAttribute = requestActionInfo.WrapResultAttribute ?? aspNetCoreOptions.DefaultWrapResultAttribute;

                if (wrapResultAttribute.WrapOnError
                    && requestActionInfo != null
                    && requestActionInfo.IsObjectResult
                    )
                {
                    await HandleAndWrapException(context, ex);
                    aspNetCoreOptions.TriggerHandledException(this, ex);

                    if (wrapResultAttribute.LogError)
                    {
                        logger.LogError(ex, ex.Message);
                    }
                    return;
                }

                throw;
            }

        }

        protected virtual async Task HandleAndWrapException(HttpContext httpContext, Exception exception)
        {
            var oldStatusCode = httpContext.Response.StatusCode;


            httpContext.Response.Clear();
            httpContext.Response.StatusCode = httpContext.GetAuthorizationException() == null ? oldStatusCode : (int)HttpStatusCode.Unauthorized;
            httpContext.Response.OnStarting(ProcessCacheHeaders, httpContext.Response);

            var errorInfo = new ErrorInfo(exception.Message, exception.ToString());
            await httpContext.Response.WriteAsync(
                JsonConvert.SerializeObject(
                            new AjaxResponse<ErrorInfo>()
                            {
                                Code = httpContext.Response.StatusCode,
                                Success = false,
                                UnAuthorizedRequest = true,
                                Result = errorInfo
                            }
                        )
                );
        }

        /// <summary>
        /// 处理请求头中缓存相关的键值
        /// </summary>
        /// <param name="state"></param>
        /// <returns></returns>
        protected virtual Task ProcessCacheHeaders(object state)
        {
            var response = (HttpResponse)state;

            response.Headers[HeaderNames.CacheControl] = "no-cache";
            response.Headers[HeaderNames.Pragma] = "no-cache";
            response.Headers[HeaderNames.Expires] = "-1";
            response.Headers.Remove(HeaderNames.ETag);

            return Task.CompletedTask;
        }
    }
}
