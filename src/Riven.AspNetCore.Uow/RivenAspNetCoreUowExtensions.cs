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
using Riven.Uow;

namespace Riven
{
    public static class RivenAspNetCoreUowExtensions
    {
        /// <summary>
        /// 添加AspNet Core UnitOfWork
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddRivenAspNetCoreUow(this IServiceCollection services, Action<UnitOfWorkAttribute> configurationDefaultUowAttr = null)
        {
            services.AddTransient<IAspNetCoreUnitOfWorkHandler, AspNetCoreUowHandler>();

            if (configurationDefaultUowAttr != null)
            {
                var unitOfWorkAttribute = new UnitOfWorkAttribute();
                configurationDefaultUowAttr.Invoke(unitOfWorkAttribute);
                services.AddSingleton<UnitOfWorkAttribute>(unitOfWorkAttribute);
            }
            else
            {
                services.AddSingleton<UnitOfWorkAttribute>();
            }


            return services;
        }
    }
}
