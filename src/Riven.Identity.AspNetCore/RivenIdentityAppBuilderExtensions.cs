using JetBrains.Annotations;

using Microsoft.AspNetCore.Builder;

using Riven.Middlewares;

namespace Riven
{
    public static class RivenIdentityAppBuilderExtensions
    {
        /// <summary>
        /// Use default authentication
        /// </summary>
        /// <param name="app"></param>
        /// <returns></returns>
        public static IApplicationBuilder UseDefaultAuthentication(this IApplicationBuilder app)
        {
            return app.UseAuthentication();
        }

        /// <summary>
        /// Use riven authentication middleware
        /// </summary>
        /// <param name="app"></param>
        /// <returns></returns>
        public static IApplicationBuilder UseRivenAuthentication(this IApplicationBuilder app)
        {
            return app.UseMiddleware<RivenAuthenticationMiddleware>();
        }
    }
}
