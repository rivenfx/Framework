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

namespace Riven.Identity.Authorization
{
    /// <summary>
    /// 基于 Claims 的 AuthorizationHandler
    /// </summary>
    public class ClaimsAuthorizationRequirement : AuthorizationHandler<ClaimsAuthorizationRequirement>, IAuthorizationRequirement
    {
        readonly IServiceProvider _serviceProvider;

        public ClaimsAuthorizationRequirement(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context,
            ClaimsAuthorizationRequirement requirement)
        {
            if (context.HasSucceeded || context.HasFailed)
            {
                return;
            }

            var routeEndpoint = context.Resource as RouteEndpoint;


            var claimsAttributes = routeEndpoint?.Metadata?.GetOrderedMetadata<ClaimsAuthorizeAttribute>()?.ToList();
            if (claimsAttributes == null || !claimsAttributes.Any())
            {
                context.Succeed(requirement);
                return;
            }

            using (var scope = _serviceProvider.CreateScope())
            {
                var serviceProvider = scope.ServiceProvider;

                var logger = serviceProvider.GetRequiredService<ILogger<ClaimsAuthorizationRequirement>>();
                var httpContextAccessor = serviceProvider.GetRequiredService<IHttpContextAccessor>();

                try
                {
                    var identityOptions = serviceProvider.GetRequiredService<IOptions<IdentityOptions>>().Value;

                    var user = httpContextAccessor.HttpContext.User;
                    var userId = user.GetUserId(identityOptions);

                    if (user == null || userId.IsNullOrWhiteSpace())
                    {
                        context.Fail();
                        return;
                    }

                    var claimsChecker = serviceProvider.GetRequiredService<IClaimsChecker>();

                    foreach (var claimsAttribute in claimsAttributes)
                    {
                        await claimsChecker.AuthorizeAsync(userId, claimsAttribute.RequireAll, claimsAttribute.Claims);
                    }

                    context.Succeed(requirement);
                }
                catch (Exception ex)
                {
                    logger.LogError(ex, ex.Message);
                    httpContextAccessor.HttpContext.SetAuthorizationException(ex);
                    context.Fail();

                    throw ex;
                }

            }
        }
    }
}
