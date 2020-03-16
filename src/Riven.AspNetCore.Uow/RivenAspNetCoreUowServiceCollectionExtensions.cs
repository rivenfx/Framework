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
    public static class RivenAspNetCoreUowServiceCollectionExtensions
    {
        /// <summary>
        /// 添加AspNet Core UnitOfWork
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddRivenAspNetCoreUow(this IServiceCollection services)
        {
            services.AddTransient<IAspNetCoreUnitOfWorkHandler, AspNetCoreUowHandler>();

            return services;
        }
    }
}
