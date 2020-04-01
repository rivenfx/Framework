using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

using Riven.Localization;
using System.Globalization;

namespace Riven
{
    public static class RivenAspNetCoreLocalizationExtensions
    {
        /// <summary>
        /// 添加Rive的请求本地化服务
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddRivenRequestLocalization(this IServiceCollection services)
        {
            services.TryAddTransient<ICultureAccessor, DefaultCultureAccessor>();
            services.TryAddTransient<ICurrentLanguage, AspNetCoreCurrentLanguage>();

            services.AddRivenLocalization();
            return services;
        }

        /// <summary>
        /// 启用Riven的请求本地化服务
        /// </summary>
        /// <param name="app"></param>
        /// <param name="optionsAction"></param>
        /// <returns></returns>
        public static IApplicationBuilder UseRivenRequestLocalization(this IApplicationBuilder app, Action<RequestLocalizationOptions> optionsAction = null)
        {
            var serviceProvider = app.ApplicationServices;
            var languageManager = serviceProvider.GetService<ILanguageManager>();

            var supportedCultures = languageManager.GetEnabledLanguages()
                .Select(o =>
                {
                    return CultureInfo.GetCultureInfo(o.Culture);
                }).ToArray();

            var options = new RequestLocalizationOptions
            {
                SupportedCultures = supportedCultures,
                SupportedUICultures = supportedCultures
            };

            //0: QueryStringRequestCultureProvider
            options.RequestCultureProviders.Insert(1, new DefaultUserRequestCultureProvider());
            options.RequestCultureProviders.Insert(2, new DefaultLocalizationHeaderRequestCultureProvider());
            //3: CookieRequestCultureProvider
            //4: AcceptLanguageHeaderRequestCultureProvider
            options.RequestCultureProviders.Insert(5, new DefaultRequestCultureProvider());


            optionsAction?.Invoke(options);


            return app.UseRequestLocalization(options);
        }
    }
}
