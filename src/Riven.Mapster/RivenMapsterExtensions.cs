using JetBrains.Annotations;
using Mapster;
using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Text;

namespace Riven
{
    public static class RivenMapsterExtensions
    {
        /// <summary>
        /// 全局映射配置 - 扫描程序集注册的对象映射
        /// </summary>
        /// <param name="assembly"></param>
        public static void RegisterGlobalObjectMapper([NotNull]this Assembly assembly)
        {
            Check.NotNull(assembly, nameof(assembly));

            TypeAdapterConfig.GlobalSettings.Scan(assembly);
        }

        /// <summary>
        /// 指定映射配置 - 扫描程序集注册的对象映射
        /// </summary>
        /// <param name="typeAdapterConfig"></param>
        /// <param name="assemblies"></param>
        public static void RegisterObjectMapper([NotNull]this TypeAdapterConfig typeAdapterConfig, params Assembly[] assemblies)
        {
            Check.NotNull(typeAdapterConfig, nameof(typeAdapterConfig));

            if (assemblies == null || assemblies.Length == 0)
            {
                return;
            }

            typeAdapterConfig.Scan(assemblies);
        }
    }
}
