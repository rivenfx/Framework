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
        /// Use jwt authentication
        /// </summary>
        /// <param name="app"></param>
        /// <returns></returns>
        public static IApplicationBuilder UseJwtAuthentication(this IApplicationBuilder app, [NotNull] string schema = "Bearer")
        {
            Check.NotNullOrWhiteSpace(schema, nameof(schema));

            return app.UseMiddleware<JwtAuthenticationMiddleware>(schema);
        }
    }
}
