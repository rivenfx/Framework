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


            return services;
        }


        /// <summary>
        /// 添加 Riven AspNetCore 相关服务
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddRivenAspNetCoreServices(this IServiceCollection services)
        {
            services.Configure<MvcOptions>((options) =>
            {
                options.Filters.AddService<RequestActionFilter>();
            });

            //if (rivenAspNetCoreOptions.AuthorizationFilterEnable)
            //{
            //    services.TryAddTransient<AppAuthorizationFilter>();
            //    services.TryAddTransient<IAspNetCoreAuthorizationHandler, NullAspNetCoreAuthorizationHandler>();
            //    services.Configure<MvcOptions>((options) =>
            //    {
            //        options.Filters.AddService<AppAuthorizationFilter>();
            //    });
            //}

            //if (rivenAspNetCoreOptions.AuditFilterEnable)
            //{
            //    services.TryAddTransient<AppAuditFilter>();
            //    services.TryAddTransient<IAspNetCoreAuditHandler, NullAspNetCoreAuditHandler>();
            //    services.Configure<MvcOptions>((options) =>
            //    {
            //        options.Filters.AddService<AppAuditFilter>();
            //    });
            //}

            //if (rivenAspNetCoreOptions.ValidationFilterEnable)
            //{
            //    services.TryAddTransient<AppValidationFilter>();
            //    services.TryAddTransient<IAspNetCoreValidationHandler, NullAspNetCoreValidationHandler>();
            //    services.Configure<MvcOptions>((options) =>
            //    {
            //        options.Filters.AddService<AppValidationFilter>();
            //    });
            //}

            //if (rivenAspNetCoreOptions.UnitOfWorkFilterEnable)
            //{
            //    services.TryAddTransient<AppUowFilter>();
            //    services.TryAddTransient<IAspNetCoreUnitOfWorkHandler, NullAspNetCoreUnitOfWorkHandler>();
            //    services.Configure<MvcOptions>((options) =>
            //    {
            //        options.Filters.AddService<AppUowFilter>();
            //    });
            //}

            //if (rivenAspNetCoreOptions.ExceptionFilterEnable)
            //{
            //    services.TryAddTransient<AppExceptionFilter>();
            //    services.TryAddTransient<IAspNetCoreExceptionHandeler, NullAspNetCoreExceptionHandeler>();
            //    services.Configure<MvcOptions>((options) =>
            //    {
            //        options.Filters.AddService<AppExceptionFilter>();
            //    });
            //}

            //if (rivenAspNetCoreOptions.ResultFilterEnable)
            //{
            //    services.TryAddTransient<AppResultFilter>();
            //    services.TryAddTransient<IAspNetCoreResultHandler, NullAspNetCoreResultHandler>();
            //    services.Configure<MvcOptions>((options) =>
            //    {
            //        options.Filters.AddService<AppResultFilter>();
            //    });
            //}

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
