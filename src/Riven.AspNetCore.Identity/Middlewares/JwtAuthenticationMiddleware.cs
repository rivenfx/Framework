using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Riven.Middlewares
{
    /// <summary>
    /// Jwt Authentication Middleware
    /// </summary>
    public class JwtAuthenticationMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly string _schema;

        public JwtAuthenticationMiddleware(RequestDelegate next, string schema)
        {
            this._next = next;
            this._schema = schema;
        }

        public async Task Invoke(HttpContext context)
        {
            if (context.User.Identity == null || context.User.Identity?.IsAuthenticated == false)
            {
                var result = await context.AuthenticateAsync(this._schema);
                if (result.Succeeded && result.Principal != null)
                {
                    context.User = result.Principal;
                }
            }

            await this._next(context);
        }
    }
}

