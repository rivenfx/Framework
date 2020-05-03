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
            if (claimsAttributes == null || claimsAttributes.Count == 0)
            {
                context.Succeed(requirement);
                return;
            }

            var scope = _serviceProvider.CreateScope();
            var serviceProvider = scope.ServiceProvider;

            try
            {

                if (!claimsAttributes.Any())
                {
                    return;
                }

                var identityOptions = serviceProvider.GetRequiredService<IOptions<IdentityOptions>>().Value;
                var httpContextAccessor = serviceProvider.GetRequiredService<IHttpContextAccessor>();


                var user = httpContextAccessor.HttpContext.User;
                var userId = user.GetUserId(identityOptions);

                if (user == null || userId.IsNullOrWhiteSpace())
                {
                    throw new AuthorizationException("CurrentUserDidNotLoginToTheApplication");
                }

                var claimsChecker = serviceProvider.GetService<IClaimsChecker>();

                foreach (var claimsAttribute in claimsAttributes)
                {
                    await claimsChecker.AuthorizeAsync(userId, claimsAttribute.RequireAll, claimsAttribute.Claims);
                }
            }
            catch (Exception ex)
            {
                context.Fail();

                var logger = serviceProvider.GetService<ILogger<ClaimsAuthorizationRequirement>>();
                logger.LogError(ex, ex.Message);
            }
            finally
            {
                scope.Dispose();
            }

        }
    }
}
