using Microsoft.EntityFrameworkCore.Metadata;

using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System.Collections.Concurrent;
using Microsoft.Extensions.Logging;
using Riven.Uow;
using Riven.Uow.Providers;

namespace Riven
{

    public interface IRivenDbContext
    {
        /// <summary>
        /// 依赖注入容器
        /// </summary>
        public IServiceProvider ServiceProvider { get; }

        /// <summary>
        /// 服务实例容器
        /// </summary>
        public ConcurrentDictionary<Type, object> SerivceInstanceMap { get; }

        /// <summary>
        /// 日志
        /// </summary>
        public ILogger Logger => this.GetApplicationService<ILogger>();

        /// <summary>
        /// Guid 生成器
        /// </summary>
        public IGuidGenerator GuidGenerator => this.GetApplicationService<IGuidGenerator>();

        /// <summary>
        /// 当前工作单元提供者
        /// </summary>
        public ICurrentUnitOfWorkProvider CurrentUnitOfWorkProvider => this.GetApplicationService<ICurrentUnitOfWorkProvider>();

        /// <summary>
        /// 获取当前租户名称
        /// </summary>
        /// <returns></returns>
        public string GetCurrentTenantNameOrNull();

        /// <summary>
        /// 获取当前用户Id
        /// </summary>
        /// <returns></returns>
        public string GetCurrentUserIdOrNull();

        /// <summary>
        /// 获取当前是否启用了多租户
        /// </summary>
        /// <returns></returns>
        public bool GetMultiTenancyEnabled();


        /// <summary>
        /// 获取服务实例
        /// </summary>
        /// <typeparam name="TService"></typeparam>
        /// <returns></returns>
        public TService GetApplicationService<TService>()
            where TService : class
        {
            if (this.ServiceProvider == null)
            {
                return default(TService);
            }

            var serviceType = typeof(TService);
            return (TService)SerivceInstanceMap.GetOrAdd(serviceType, (type) =>
            {
                return this.ServiceProvider.GetService(serviceType);
            });
        }

        /// <summary>
        /// 释放 IRivenDbContext 资源
        /// </summary>
        public void DisposeRivenDbContext()
        {
            SerivceInstanceMap.Clear();
        }
    }
}
