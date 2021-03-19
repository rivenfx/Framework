using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Options;

using Riven.AspNetCore.FilterHandlers;
using Riven.Authorization;
using Riven.Identity;
using Riven.Identity.Authorization;
using Riven.Identity.Permissions;
using Riven.Identity.Users;

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
            // 认证策略
            services.TryAddSingleton<IAuthorizationPolicyProvider, PermissionAuthorizationPolicyProvider>();

            // 权限检查器
            services.TryAddTransient<IPermissionChecker, AspNetCorePermissionChecker>();

            return services;
        }


        /// <summary>
        /// 添加权限存储器
        /// </summary>
        /// <typeparam name="TPermissionStore"></typeparam>
        /// <typeparam name="TPermission"></typeparam>
        /// <param name="identityBuilder"></param>
        /// <returns></returns>
        public static IdentityBuilder AddPermissionStore<TPermissionStore, TPermission>(this IdentityBuilder identityBuilder)
            where TPermission : IdentityPermission
            where TPermissionStore : IIdentityPermissionStore<TPermission>
        {
            var userStoreType = typeof(IUserStore<>).MakeGenericType(identityBuilder.UserType);
            var roleStoreType = typeof(IRoleStore<>).MakeGenericType(identityBuilder.RoleType);

            var identityPermissionStoreType = typeof(IIdentityPermissionStore<>);




            // 权限存储器
            identityBuilder.Services
                .TryAddScoped(
                    identityPermissionStoreType,
                    typeof(TPermissionStore)
                );

            // 权限查找器
            identityBuilder.Services.TryAddScoped((provider) =>
            {
                return (IIdentityPermissionFinder)provider.GetRequiredService(identityPermissionStoreType);
            });

            // 用户角色查找器
            identityBuilder.Services.TryAddScoped((provider) =>
            {
                var obj = provider.GetRequiredService(userStoreType);
                if (obj is IIdentityUserRoleFinder res)
                {
                    return res;
                }


                throw new Exception(
                    $"{userStoreType.FullName} does not implement the {typeof(IIdentityUserRoleFinder).FullName} interface."
                    );
            });

            return identityBuilder;
        }
    }
}
