using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Riven.AspNetCore.FilterHandlers;
using Riven.Authorization;

namespace Riven
{
    public static class RivenIdentityServiceExtensions
    {
        /// <summary>
        /// 添加 IAspNetCoreAuthorizationHandler 的 ClaimsAuthorizationHandler 实现
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddRivenAspNetCoreClaimsAuthorization(this IServiceCollection services)
        {
            services.Replace(new ServiceDescriptor(
                typeof(IAspNetCoreAuthorizationHandler),
                typeof(ClaimsAuthorizationHandler),
                ServiceLifetime.Transient)
                );

            return services;
        }

        /// <summary>
        /// 添加 Riven Identity 的 ClaimAccessor 实现
        /// </summary>
        /// <typeparam name="TRoleClaimAccessor"></typeparam>
        /// <typeparam name="TUserRoleClaimAccessor"></typeparam>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddRivenIdentityClaimAccesssor<TRoleClaimAccessor, TUserRoleClaimAccessor>(this IServiceCollection services)
            where TRoleClaimAccessor : class, IRoleClaimAccessor
            where TUserRoleClaimAccessor : class, IUserRoleClaimAccessor
        {
            services.TryAddScoped<IRoleClaimAccessor, TRoleClaimAccessor>();
            services.TryAddScoped<IUserRoleClaimAccessor, TUserRoleClaimAccessor>();

            return services;
        }
    }
}
