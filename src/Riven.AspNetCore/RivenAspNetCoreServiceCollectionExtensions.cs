using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System.Linq;
using Riven.AspNetCore.Mvc.Uow;
using Riven.AspNetCore.FilterHandlers;
using Riven.AspNetCore.Mvc.ExceptionHandling;
using Microsoft.AspNetCore.Mvc;
using Riven.AspNetCore.Mvc.Authorization;
using Riven.AspNetCore.Mvc.Auditing;
using Riven.AspNetCore.Mvc.Results;
using Riven.AspNetCore.Mvc.Validation;

namespace Riven
{
    public static class RivenUnitOfWorkServiceCollectionExtensions
    {
        /// <summary>
        /// 添加AspNet Core相关配置
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddRivenAspNetCore(this IServiceCollection services)
        {
            services.Configure<MvcOptions>((options) =>
            {
                options.Filters.AddService<AppAuthorizationFilter>();
                options.Filters.AddService<AppAuditActionFilter>();

                options.Filters.AddService(typeof(AppValidationActionFilter));

                options.Filters.AddService<AppUowFilter>();
                options.Filters.AddService<AppExceptionFilter>();
                options.Filters.AddService<AppResultFilter>();
            });

            services.AddRivenAspNetCoreServices();

            return services;
        }

        static IServiceCollection AddRivenAspNetCoreServices(this IServiceCollection services)
        {
            services.TryAddTransient<AppAuthorizationFilter>();
            services.TryAddTransient<AppAuditActionFilter>();
            services.TryAddTransient<AppValidationActionFilter>();
            services.TryAddTransient<AppUowFilter>();
            services.TryAddTransient<AppExceptionFilter>();
            services.TryAddTransient<AppResultFilter>();


            services.TryAddTransient<IAspNetCoreAuthorizationHandler, NullAspNetCoreAuthorizationHandler>();
            services.TryAddTransient<IAspNetCoreAuditHandler, NullAspNetCoreAuditHandler>();
            services.TryAddTransient<IAspNetCoreValidationHandler, NullAspNetCoreValidationHandler>();
            services.TryAddTransient<IAspNetCoreUnitOfWorkHandler, NullAspNetCoreUnitOfWorkHandler>();
            services.TryAddTransient<IAspNetCoreExceptionHandeler, NullAspNetCoreExceptionHandeler>();
            services.TryAddTransient<IAspNetCoreResultHandler, NullAspNetCoreResultHandler>();

            return services;
        }
    }
}
