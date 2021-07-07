using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Riven.Middlewares
{
    /// <summary>
    /// Riven Authentication Middleware
    /// </summary>
    public class RivenAuthenticationMiddleware
    {
        private readonly RequestDelegate _next;

        public RivenAuthenticationMiddleware(RequestDelegate next)
        {
            this._next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            // 如果已经认证成功，那么跳过
            if (context.User.Identity != null && context.User.Identity.IsAuthenticated)
            {
                await this._next(context);
                return;
            }


            // 遍历所有的scheme进行认证
            var authenticationSchemeProvider = context.RequestServices.GetRequiredService<IAuthenticationSchemeProvider>();
            var authenticationSchemes = await authenticationSchemeProvider.GetAllSchemesAsync();
            foreach (var scheme in authenticationSchemes)
            {
                if (context.User.Identity == null || context.User.Identity?.IsAuthenticated == false)
                {
                    var result = await context.AuthenticateAsync(scheme.Name);
                    if (result.Succeeded && result.Principal != null)
                    {
                        context.User = result.Principal;
                        break;
                    }
                }
            }
            await this._next(context);
        }
    }
}

