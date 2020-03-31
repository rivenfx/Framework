using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Riven.Identity.Permissions
{

    public class PermissionAttribute : Attribute
    {

    }

    public class RoleClaimAuthorizationRequirement : IAuthorizationRequirement
    {

    }

    public class RoleClaimAuthorizationHandler : AuthorizationHandler<RoleClaimAuthorizationRequirement>
    {
        public RoleClaimAuthorizationHandler()
        {

        }

        protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, RoleClaimAuthorizationRequirement requirement)
        {
            if (context.HasSucceeded || context.HasFailed)
            {
                return;
            }

            var routeEndpoint = context.Resource as RouteEndpoint;
            var permissionAttr = routeEndpoint.Metadata.GetMetadata<PermissionAttribute>();

            if (permissionAttr == null)
            {
                return;
            }

            var roles = context.User.FindAll(o => o.Type == ClaimTypes.Role).Select(o => o.Value).ToArray();
            if (roles.Length == 0)
            {
                context.Fail();
                return;
            }




            throw new NotImplementedException();
        }
    }


    /// <summary>
    /// 权限
    /// </summary>
    public class AppPermissions
    {
        public AppPermissions()
        {

        }

        /// <summary>
        /// 父级权限名称
        /// </summary>
        public string ParentName { get; set; }

        /// <summary>
        /// 权限名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 显示的权限名称
        /// </summary>
        public string DisplayName { get; set; }

        /// <summary>
        /// 描述信息
        /// </summary>
        public string Description { get; set; }
    }
}
