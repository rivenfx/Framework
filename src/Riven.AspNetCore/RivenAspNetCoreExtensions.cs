using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System.Linq;
using Riven.AspNetCore.FilterHandlers;
using Riven.AspNetCore.Mvc.ExceptionHandling;
using Microsoft.AspNetCore.Mvc;
using Riven.AspNetCore.Mvc.Results;
using Riven.AspNetCore.Mvc.Validation;
using Riven.Configuration;
using Microsoft.AspNetCore.Builder;
using Riven.AspNetCore.Mvc.Request;
using Riven.AspNetCore.Mvc.Results.Wrapping;

namespace Riven
{
    public static class RivenAspNetCoreExtensions
    {
        /// <summary>
        /// 添加 Riven AspNet Core 相关配置
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configurationAction"></param>
        /// <returns></returns>
        public static IServiceCollection AddRivenAspNetCore(this IServiceCollection services, Action<RivenAspNetCoreOptions> configurationAction = null)
        {
            services.AddOptions<RivenAspNetCoreOptions>();

            services.Configure(configurationAction);

            services.TryAddSingleton<IRequestActionResultWrapperFactory, DefaultRequestActionResultWrapperFactory>();

            services.TryAddTransient<ExceptionHandlingMiddleware>();

            return services;
        }


        /// <summary>
        /// 添加 Riven AspNetCore 相关过滤器
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddRivenAspNetCoreFilters(this IServiceCollection services)
        {
            services.AddTransient<RequestActionFilter>();
            services.Configure<MvcOptions>((options) =>
            {
                options.Filters.AddService<RequestActionFilter>();
            });

            return services;
        }


        /// <summary>
        /// 添加riven的异常处理器
        /// </summary>
        /// <param name="app"></param>
        /// <returns></returns>
        public static IApplicationBuilder UseRivenAspNetCoreExceptionHandling(this IApplicationBuilder app)
        {
            app.UseMiddleware<ExceptionHandlingMiddleware>();

            return app;
        }
    }
}
