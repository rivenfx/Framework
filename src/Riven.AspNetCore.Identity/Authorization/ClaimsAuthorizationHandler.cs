using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using Riven.AspNetCore.FilterHandlers;
using Riven.AspNetCore.Mvc.Extensions;
using Riven.AspNetCore.Mvc.Results;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Riven.AspNetCore.Models;
using System.Net;
using Riven.Reflection;
using System.Reflection;
using System.Linq;
using Riven.Identity.Authorization;
using Microsoft.AspNetCore.Identity;
using Riven.Extensions;
using Microsoft.Extensions.Options;

namespace Riven.Authorization
{
    public class ClaimsAuthorizationHandler : IAspNetCoreAuthorizationHandler
    {
        readonly IServiceProvider _serviceProvider;
        readonly IdentityOptions _identityOptions;
        readonly IHttpContextAccessor _httpContextAccessor;
        readonly ILogger<ClaimsAuthorizationHandler> _logger;

        public ClaimsAuthorizationHandler(IServiceProvider serviceProvider, IOptions<IdentityOptions> identityOptionsWrap, IHttpContextAccessor httpContextAccessor, ILogger<ClaimsAuthorizationHandler> logger)
        {
            _serviceProvider = serviceProvider;
            _identityOptions = identityOptionsWrap.Value;
            _httpContextAccessor = httpContextAccessor;
            _logger = logger;
        }

        public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {
            var endpoint = context?.HttpContext?.GetEndpoint();
            // Allow Anonymous skips all authorization
            if (endpoint?.Metadata.GetMetadata<IAllowAnonymous>() != null)
            {
                return;
            }

            if (!context.ActionDescriptor.IsControllerAction())
            {
                return;
            }


            //TODO: Avoid using try/catch, use conditional checking
            try
            {
                var methodInfo = context.ActionDescriptor.GetMethodInfo();
                var methodInfoDeclaringType = context.ActionDescriptor.GetMethodInfo().DeclaringType;
                if (AllowAnonymous(methodInfo, methodInfoDeclaringType))
                {
                    return;
                }

                if (ReflectionHelper.IsPropertyGetterSetterMethod(methodInfo, methodInfoDeclaringType))
                {
                    return;
                }

                await Authorize(methodInfo, methodInfoDeclaringType);
            }
            catch (AuthorizationException ex)
            {
                _logger.LogWarning(ex.ToString(), ex);

                //_eventBus.Trigger(this, new AbpHandledExceptionData(ex));

                if (ActionResultHelper.IsObjectResult(context.ActionDescriptor.GetMethodInfo().ReturnType))
                {
                    var errorInfo = new ErrorInfo(
                            (int)HttpStatusCode.Unauthorized,
                            ex.Message,
                            ex.ToString()
                        );
                    context.Result = new ObjectResult(new AjaxResponse(errorInfo, true))
                    {
                        StatusCode = context.HttpContext.User.Identity.IsAuthenticated
                            ? (int)HttpStatusCode.Forbidden
                            : (int)HttpStatusCode.Unauthorized
                    };
                }
                else
                {
                    context.Result = new ChallengeResult();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString(), ex);


                if (ActionResultHelper.IsObjectResult(context.ActionDescriptor.GetMethodInfo().ReturnType))
                {
                    var errorInfo = new ErrorInfo((int)HttpStatusCode.Forbidden, ex.Message, ex.ToString());
                    context.Result = new ObjectResult(new AjaxResponse(errorInfo))
                    {
                        StatusCode = (int)HttpStatusCode.InternalServerError
                    };
                }
                else
                {
                    //TODO: How to return Error page?
                    context.Result = new StatusCodeResult(
                            (int)HttpStatusCode.InternalServerError
                        );
                }
            }




        }

        protected virtual async Task Authorize(MethodInfo methodInfo, Type type)
        {
            if (!methodInfo.IsPublic && !methodInfo.GetCustomAttributes().OfType<ClaimsAuthorizeAttribute>().Any())
            {
                return;
            }

            var authorizeAttributes = ReflectionHelper
                        .GetAttributesOfMemberAndType(methodInfo, type)
                        .OfType<ClaimsAuthorizeAttribute>()
                        .ToArray();

            if (!authorizeAttributes.Any())
            {
                return;
            }


            var user = _httpContextAccessor.HttpContext.User;
            var userId = user.GetUserId(this._identityOptions);

            if (user == null || userId.IsNullOrWhiteSpace())
            {
                throw new AuthorizationException("CurrentUserDidNotLoginToTheApplication");
            }

            using (var serviceScope = _serviceProvider.CreateScope())
            {
                var serviceProvider = serviceScope.ServiceProvider;
                var claimsChecker = serviceProvider.GetService<IClaimsChecker>();

                foreach (var authorizeAttribute in authorizeAttributes)
                {
                    await claimsChecker.AuthorizeAsync(userId, authorizeAttribute.RequireAll, authorizeAttribute.Claims);
                }
            }
        }

        protected static bool AllowAnonymous(MemberInfo memberInfo, Type type)
        {
            return ReflectionHelper
                .GetAttributesOfMemberAndType(memberInfo, type)
                .OfType<AllowAnonymousAttribute>()
                .Any();
        }
    }
}
