using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

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
            services.TryAddSingleton<ILanguageManager, DefaultLanguageManager>();
            services.TryAddSingleton<ILocalizationManager, DefaultLocalizationManager>();
            services.TryAddTransient<ICurrentLanguage, NullCurrentLanguage>();

            return services;
        }
    }
}
