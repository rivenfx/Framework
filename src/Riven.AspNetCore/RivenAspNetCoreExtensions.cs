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
using Riven.Configuration;

namespace Riven
{
    public static class RivenAspNetCoreExtensions
    {
        /// <summary>
        /// 添加Riven AspNet Core相关配置
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configurationAction"></param>
        /// <returns></returns>
        public static IServiceCollection AddRivenAspNetCore(this IServiceCollection services, Action<RivenAspNetCoreOptions> configurationAction = null)
        {
            var rivenAspNetCoreOptions = new RivenAspNetCoreOptions();
            configurationAction?.Invoke(rivenAspNetCoreOptions);

            services.AddRivenAspNetCoreServices(rivenAspNetCoreOptions);

            return services;
        }

        static IServiceCollection AddRivenAspNetCoreServices(this IServiceCollection services, RivenAspNetCoreOptions rivenAspNetCoreOptions)
        {
            if (rivenAspNetCoreOptions.AuthorizationFilterEnable)
            {
                services.TryAddTransient<AppAuthorizationFilter>();
                services.TryAddTransient<IAspNetCoreAuthorizationHandler, NullAspNetCoreAuthorizationHandler>();
                services.Configure<MvcOptions>((options) =>
                {
                    options.Filters.AddService<AppAuthorizationFilter>();
                });
            }

            if (rivenAspNetCoreOptions.AuditFilterEnable)
            {
                services.TryAddTransient<AppAuditFilter>();
                services.TryAddTransient<IAspNetCoreAuditHandler, NullAspNetCoreAuditHandler>();
                services.Configure<MvcOptions>((options) =>
                {
                    options.Filters.AddService<AppAuditFilter>();
                });
            }

            if (rivenAspNetCoreOptions.ValidationFilterEnable)
            {
                services.TryAddTransient<AppValidationFilter>();
                services.TryAddTransient<IAspNetCoreValidationHandler, NullAspNetCoreValidationHandler>();
                services.Configure<MvcOptions>((options) =>
                {
                    options.Filters.AddService<AppValidationFilter>();
                });
            }

            if (rivenAspNetCoreOptions.UnitOfWorkFilterEnable)
            {
                services.TryAddTransient<AppUowFilter>();
                services.TryAddTransient<IAspNetCoreUnitOfWorkHandler, NullAspNetCoreUnitOfWorkHandler>();
                services.Configure<MvcOptions>((options) =>
                {
                    options.Filters.AddService<AppUowFilter>();
                });
            }

            if (rivenAspNetCoreOptions.ExceptionFilterEnable)
            {
                services.TryAddTransient<AppExceptionFilter>();
                services.TryAddTransient<IAspNetCoreExceptionHandeler, NullAspNetCoreExceptionHandeler>();
                services.Configure<MvcOptions>((options) =>
                {
                    options.Filters.AddService<AppExceptionFilter>();
                });
            }

            if (rivenAspNetCoreOptions.ResultFilterEnable)
            {
                services.TryAddTransient<AppResultFilter>();
                services.TryAddTransient<IAspNetCoreResultHandler, NullAspNetCoreResultHandler>();
                services.Configure<MvcOptions>((options) =>
                {
                    options.Filters.AddService<AppResultFilter>();
                });
            }

            return services;
        }
    }
}
