
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection.Extensions;

using Riven.Authorization;

namespace Riven
{
    public static class RivenIdentityServiceExtensions
    {
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
