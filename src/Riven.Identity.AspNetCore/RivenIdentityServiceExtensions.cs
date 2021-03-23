
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

using Riven.Authorization;
using Riven.Identity.Permissions;

using System;

namespace Riven
{
    public static class RivenIdentityServiceExtensions
    {
        /// <summary>
        /// 添加 Riven Identity AspNetCore 服务
        /// </summary>
        /// <typeparam name="TUser"></typeparam>
        /// <typeparam name="TRole"></typeparam>
        /// <typeparam name="TPermission"></typeparam>
        /// <param name="services"></param>
        /// <param name="setupAction"></param>
        /// <returns></returns>
        public static IdentityBuilder AddRivenIdentity<TUser, TRole, TPermission>(this IServiceCollection services, Action<IdentityOptions> setupAction = null)
            where TUser : class
            where TRole : class
            where TPermission : IdentityPermission
        {
            var builder = services.AddIdentity<TUser, TRole>((options) =>
            {
                options.ConfigureRivenIdentity<TPermission>();
                setupAction?.Invoke(options);
            });
            return builder.AddRivenIdentityCore<TPermission>();
        }


        /// <summary>
        /// 添加 IAspNetCoreAuthorizationHandler 的 PermissionAuthorizationHandler 实现
        /// </summary>
        /// <param name="identityBuilder"></param>
        /// <returns></returns>
        public static IdentityBuilder AddRivenAspNetCorePermissionAuthorization(this IdentityBuilder identityBuilder)
        {

            // 权限认证策略
            identityBuilder.Services.TryAddScoped<IAuthorizationPolicyProvider, PermissionAuthorizationPolicyProvider>();

            return identityBuilder;
        }
    }
}
