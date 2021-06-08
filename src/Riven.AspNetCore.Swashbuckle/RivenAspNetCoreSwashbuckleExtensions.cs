using System;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.SwaggerGen;
using Panda.DynamicWebApi;
using Microsoft.AspNetCore.Builder;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerUI;

namespace Riven
{
    public static class RivenAspNetCoreSwashbuckleExtensions
    {
        /// <summary>
        /// 添加AspNet Core web api相关配置
        /// </summary>
        /// <param name="services"></param>
        /// <param name="swaggerConfigurationAction">swagger配置</param>
        /// <param name="dynamicWebApiConfigurationAction">panda.DynamicWebApi配置</param>
        /// <returns></returns>
        public static IServiceCollection AddRivenAspNetCoreSwashbuckle(this IServiceCollection services, Action<SwaggerGenOptions> swaggerConfigurationAction = null, Action<DynamicWebApiOptions> dynamicWebApiConfigurationAction = null)
        {
            // swagger
            services.AddSwaggerGen((options) =>
            {
                options.DocInclusionPredicate((docName, description) => true);

                swaggerConfigurationAction?.Invoke(options);
            });

            // DynamicWebApi 
            services.AddDynamicWebApi(dynamicWebApiConfigurationAction);

            return services;
        }


        /// <summary>
        /// 添加swagger 和 swaggerui
        /// </summary>
        /// <param name="app"></param>
        /// <param name="swaggerUIConfigurationAction"></param>
        /// <param name="enableSwaggerUI"></param>
        public static void UseRivenAspNetCoreSwashbuckle(this IApplicationBuilder app, Action<SwaggerUIOptions> swaggerUIConfigurationAction = null, bool enableSwaggerUI = true)
        {
            app.UseRivenAspNetCoreSwashbuckle(null, swaggerUIConfigurationAction, enableSwaggerUI);
        }

        /// <summary>
        /// 添加swagger 和 swaggerui
        /// </summary>
        /// <param name="app"></param>
        /// <param name="swaggerConfigurationAction"></param>
        /// <param name="swaggerUIConfigurationAction"></param>
        /// <param name="enableSwaggerUI"></param>
        public static void UseRivenAspNetCoreSwashbuckle(this IApplicationBuilder app, Action<SwaggerOptions> swaggerConfigurationAction = null, Action<SwaggerUIOptions> swaggerUIConfigurationAction = null, bool enableSwaggerUI = true)
        {
            app.UseSwagger((options) =>
            {
                swaggerConfigurationAction?.Invoke(options);
            });


            if (enableSwaggerUI)
            {
                app.UseSwaggerUI(options =>
                {
                    swaggerUIConfigurationAction?.Invoke(options);
                });
            }
        }
    }
}
