using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Http;
using Riven.Extensions;
using Riven.Authorization;
using Riven.Configuration;
using Riven.AspNetCore.Models;
using Newtonsoft.Json;
using System.Net;
using Riven.AspNetCore.Mvc.Extensions;
using Microsoft.Extensions.Localization;
using Riven.Exceptions;

namespace Riven.Identity.Authorization
{
    /// <summary>
    /// 基于 Permission 的 AuthorizationHandler
    /// </summary>
    public class PermissionAuthorizationRequirement : AuthorizationHandler<PermissionAuthorizationRequirement>, IAuthorizationRequirement
    {
        readonly IServiceProvider _serviceProvider;

        public PermissionAuthorizationRequirement(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context,
            PermissionAuthorizationRequirement requirement)
        {
            if (context.HasSucceeded || context.HasFailed)
            {
                return;
            }

            var routeEndpoint = context.Resource as RouteEndpoint;


            var permissionAttributes = routeEndpoint?.Metadata?.GetOrderedMetadata<PermissionAuthorizeAttribute>()?.ToList();
            if (permissionAttributes == null || !permissionAttributes.Any())
            {
                context.Succeed(requirement);
                return;
            }

            using (var scope = _serviceProvider.CreateScope())
            {
                var serviceProvider = scope.ServiceProvider;

                var logger = serviceProvider.GetRequiredService<ILogger<PermissionAuthorizationRequirement>>();
                var httpContextAccessor = serviceProvider.GetRequiredService<IHttpContextAccessor>();

                var stringLocalizer = serviceProvider.GetRequiredService<IStringLocalizer>();

                try
                {
                    var identityOptions = serviceProvider.GetRequiredService<IOptions<IdentityOptions>>().Value;

                    var user = httpContextAccessor.HttpContext.User;
                    var userId = user.GetUserId(identityOptions);

                    if (user == null || userId.IsNullOrWhiteSpace())
                    {
                        if (!httpContextAccessor.HttpContext.IsAjax())
                        {
                            context.Fail();
                            return;
                        }

                        // TODO: 本地化异常
                        throw new AuthorizationException(stringLocalizer["NotLoggedIn"]);
                    }

                    var permissionChecker = serviceProvider.GetRequiredService<IPermissionChecker>();

                    foreach (var permissionAttribute in permissionAttributes)
                    {
                        await permissionChecker.AuthorizeAsync(stringLocalizer, userId, permissionAttribute.RequireAll, permissionAttribute.Permissions);
                    }

                    context.Succeed(requirement);
                }
                catch (Exception ex)
                {
                    logger.LogError(ex, ex.Message);
                    httpContextAccessor.HttpContext.SetAuthorizationException(ex);
                    throw ex;
                }

            }
        }
    }
}
