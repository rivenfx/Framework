using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

using Riven.Data;
using Riven.Dependency;

using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Riven
{
    public static class RivenDataExtensions
    {
        /// <summary>
        /// 添加种子数据
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddRivenDataSeed(this IServiceCollection services)
        {
            // 种子数据执行器
            services.TryAddTransient<IDataSeeder, DataSeeder>();

            return services;
        }
    }
}
