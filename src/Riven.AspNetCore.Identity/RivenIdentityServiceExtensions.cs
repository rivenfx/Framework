using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Riven.AspNetCore.FilterHandlers;
using Riven.Authorization;
using Riven.Identity.Authorization;

namespace Riven
{
    public static class RivenIdentityServiceExtensions
    {
        /// <summary>
        /// 添加 IAspNetCoreAuthorizationHandler 的 PermissionAuthorizationHandler 实现
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddRivenAspNetCorePermissionAuthorization(this IServiceCollection services)
        {
            services.AddSingleton<IAuthorizationPolicyProvider, PermissionAuthorizationPolicyProvider>();

            services.TryAddTransient<IPermissionChecker, DefaultPermissionChecker>();

            return services;
        }

        /// <summary>
        /// 添加 Riven Identity 的 PermissionAccessor 实现
        /// </summary>
        /// <typeparam name="TRolePermissionAccessor"></typeparam>
        /// <typeparam name="TUserRolePermissionAccessor"></typeparam>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddRivenIdentityPermissionAccesssor<TRolePermissionAccessor, TUserRolePermissionAccessor>(this IServiceCollection services)
            where TRolePermissionAccessor : class, IRolePermissionAccessor
            where TUserRolePermissionAccessor : class, IUserRolePermissionAccessor
        {
            services.TryAddScoped<IRolePermissionAccessor, TRolePermissionAccessor>();
            services.TryAddScoped<IUserRolePermissionAccessor, TUserRolePermissionAccessor>();

            return services;
        }
    }
}
