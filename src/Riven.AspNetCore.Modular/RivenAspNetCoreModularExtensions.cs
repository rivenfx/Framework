using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Builder;

using Riven.Modular;
using Riven.AspNetCore.Accessors;

namespace Riven
{
    public static class RivenAspNetCoreModularExtensions
    {
        /// <summary>
        /// 添加模块化
        /// </summary>
        /// <typeparam name="TModule">模块类型</typeparam>
        /// <param name="services"></param>
        /// <param name="configuration">应用配置</param>
        /// <returns></returns>
        public static IServiceCollection AddRivenAspNetCoreModule<TModule>(this IServiceCollection services, IConfiguration configuration) where TModule : IAppModule
        {
            services.AddSingleton<IApplicationBuilderAccessor, DefaultApplicationBuilderAccessor>();
            services.AddRivenModule<TModule>(configuration);
            return services;
        }

        /// <summary>
        /// 启用模块化
        /// </summary>
        /// <param name="app"></param>
        /// <returns></returns>
        public static IApplicationBuilder UseRivenAspNetCoreModule(this IApplicationBuilder app)
        {
            app.ApplicationServices.GetService<IApplicationBuilderAccessor>().ApplicationBuilder = app;
            app.ApplicationServices.UseRivenModule();

            return app;
        }
    }
}
