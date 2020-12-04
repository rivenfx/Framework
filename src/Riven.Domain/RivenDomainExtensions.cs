using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

using Riven.MultiTenancy;

namespace Riven
{
    public static class RivenDomainExtensions
    {
        /// <summary>
        /// 添加Riven的多租户配置
        /// </summary>
        /// <param name="services"></param>
        /// <param name="optionConfiguration"></param>
        /// <returns></returns>
        public static IServiceCollection AddRivenMultiTenancyOptions(this IServiceCollection services, Action<IMultiTenancyOptions> optionConfiguration = null)
        {
            var multiTenancyOptions = new DefaultMultiTenancyOptions();
            optionConfiguration?.Invoke(multiTenancyOptions);
            services.TryAddSingleton<IMultiTenancyOptions>(multiTenancyOptions);


            return services;
        }

        /// <summary>
        /// 添加多租户提供者
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddRivenMultiTenancyProvider<TProvider>(this IServiceCollection services)
            where TProvider : class, IMultiTenancyProvider
        {
            services.TryAddScoped<IMultiTenancyProvider, TProvider>();

            return services;
        }
    }
}
