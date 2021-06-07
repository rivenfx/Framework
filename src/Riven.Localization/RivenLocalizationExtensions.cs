using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

using Riven.Localization;

namespace Riven
{
    public static class RivenLocalizationExtensions
    {
        /// <summary>
        /// 添加Riven的Localization库
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddRivenLocalization(this IServiceCollection services)
        {
            services.TryAddTransient<ILanguageManager, DefaultLanguageManager>();
            services.TryAddTransient<ILocalizationManager, DefaultLocalizationManager>();
            services.TryAddTransient<ICurrentLanguage, NullCurrentLanguage>();

            return services;
        }

        /// <summary>
        /// 添加语言
        /// </summary>
        /// <param name="serviceProvider"></param>
        /// <param name="languages"></param>
        /// <returns></returns>
        public static IServiceProvider AddOrUpdateLanguages(this IServiceProvider serviceProvider, params LanguageInfo[] languages)
        {
            return serviceProvider.AddOrUpdateLanguages(languages?.ToList());
        }

        /// <summary>
        /// 添加语言
        /// </summary>
        /// <param name="serviceProvider"></param>
        /// <param name="languages"></param>
        /// <returns></returns>
        public static IServiceProvider AddOrUpdateLanguages(this IServiceProvider serviceProvider, IList<LanguageInfo> languages)
        {
            if (languages == null || languages.Count == 0)
            {
                return serviceProvider;
            }

            var languageManager = serviceProvider.GetRequiredService<ILanguageManager>();

            languageManager.AddOrUpdateRange(languages.ToList());

            return serviceProvider;
        }

        /// <summary>
        /// 设置默认语言
        /// </summary>
        /// <param name="serviceProvider"></param>
        /// <param name="culture">culture</param>
        /// <returns></returns>
        public static IServiceProvider SetDefaultLanguage(this IServiceProvider serviceProvider, string culture)
        {
            var languageManager = serviceProvider.GetRequiredService<ILanguageManager>();
            languageManager.ChangeDefaultLanguage(culture);

            return serviceProvider;
        }

        /// <summary>
        /// 本地化是否启动
        /// </summary>
        /// <param name="serviceProvider"></param>
        /// <returns></returns>
        public static bool RivenLocalizationEnabled(this IServiceProvider serviceProvider)
        {
            return serviceProvider.GetService<ILanguageManager>() != null;
        }
    }
}
