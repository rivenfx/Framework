using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

using Riven.Localization;
using System.Globalization;
using Microsoft.Extensions.Localization;
using Microsoft.AspNetCore.Localization;

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
            services.TryAddTransient<IStringLocalizer, AspNetCoreStringLocalizer>();

            services.AddRivenLocalization();
            return services;
        }

        /// <summary>
        /// 启用Riven的请求本地化服务
        /// </summary>
        /// <param name="app"></param>
        /// <param name="optionsAction"></param>
        /// <param name="defaultCultures">默认支持的语言</param>
        /// <returns></returns>
        public static IApplicationBuilder UseRivenRequestLocalization(this IApplicationBuilder app, Action<RequestLocalizationOptions> optionsAction = null, string defaultCultures = "zh-Hans")
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
                SupportedUICultures = supportedCultures,
            };
            // 本地化默认使用的语言信息
            var defaultCultureInfo = supportedCultures.FirstOrDefault(o => o.Name == defaultCultures);
            if (defaultCultureInfo != null)
            {
                options.DefaultRequestCulture = new RequestCulture(
                    defaultCultureInfo,
                    defaultCultureInfo
                    );
            }

            // 0: QueryStringRequestCultureProvider
            options.RequestCultureProviders.Insert(1, new DefaultUserRequestCultureProvider(options));


            // 自行配置
            optionsAction?.Invoke(options);


            return app.UseRequestLocalization(options);
        }
    }
}
