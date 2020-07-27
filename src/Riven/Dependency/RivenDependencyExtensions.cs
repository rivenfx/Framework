using Microsoft.Extensions.DependencyInjection;
using Riven.Dependency;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Riven
{
    /// <summary>
    /// Riven 依赖注入扩展
    /// </summary>
    public static class RivenDependencyExtensions
    {
        /// <summary>
        /// 根据类型自动依赖注入
        /// </summary>
        /// <typeparam name="TType"></typeparam>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection RegisterAssemblyOf<TType>(this IServiceCollection services)
            where TType : class
        {
            return services.RegisterAssembly(typeof(TType).Assembly);
        }

        /// <summary>
        /// 根据Assembly自动依赖注入
        /// </summary>
        /// <param name="services"></param>
        /// <param name="assemblies"></param>
        /// <returns></returns>
        public static IServiceCollection RegisterAssembly(this IServiceCollection services, params Assembly[] assemblies)
        {
            Check.NotNull(assemblies, nameof(assemblies));

            services.Scan((scan) =>
            {

                scan.FromAssemblies(assemblies)
                    // 瞬时
                    .AddClasses((classes) =>
                    {
                        classes.AssignableTo<ITransientDependency>();
                    })
                    .AsSelf()
                    .AsImplementedInterfaces()
                    .WithTransientLifetime()

                     // 单例
                     .AddClasses((classes) =>
                     {
                         classes.AssignableTo<ISingletonDependency>();
                     })
                     .AsSelf()
                     .AsImplementedInterfaces()
                     .WithSingletonLifetime()

                      // 范围
                      .AddClasses((classes) =>
                      {
                          classes.AssignableTo<IScopeDependency>();
                      })
                     .AsSelf()
                     .AsImplementedInterfaces()
                     .WithScopedLifetime();
            });

            return services;
        }
    }
}
