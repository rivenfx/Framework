using System;
using System.Collections.Generic;
using System.Text;
using AspectCore.Configuration;
using Riven.Interceptors;
using Riven.Uow.Extensions;

namespace Riven
{
    public static class RivenAspectCoreExtensions
    {
        /// <summary>
        /// 添加 Riven UnitOfWork 拦截器
        /// </summary>
        /// <param name="aspectConfiguration"></param>
        /// <returns></returns>
        public static IAspectConfiguration AddRivenUnitOfWorkInterceptor(
            [JetBrains.Annotations.NotNull]this IAspectConfiguration aspectConfiguration
            )
        {
            Check.NotNull(aspectConfiguration, nameof(aspectConfiguration));

            aspectConfiguration.Interceptors.AddTyped<RivenAspectCoreUnitOfWorkInterceptor>((method) =>
            {
                return method.GetUnitOfWorkAttributeOrNull() != null;
            });

            return aspectConfiguration;
        }
    }
}
