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

            var languageManager = serviceProvider.GetService<ILanguageManager>();

            languageManager.AddOrUpdateRange(languages.ToList());

            return serviceProvider;
        }
    }
}
