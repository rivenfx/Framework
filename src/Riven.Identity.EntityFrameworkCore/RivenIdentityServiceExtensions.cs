using Microsoft.AspNetCore.Identity;

using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Riven.Identity;

namespace Riven
{
    public static class RivenIdentityEfCoreServiceExtensions
    {
        /// <summary>
        /// 添加数据库上下文访问器
        /// </summary>
        /// <typeparam name="TDbContextAccessor"></typeparam>
        /// <param name="builder"></param>
        /// <returns></returns>
        public static IdentityBuilder AddDbContextAccessor<TDbContextAccessor>(this IdentityBuilder builder)
            where TDbContextAccessor : class, IIdentityDbContextAccessor
        {
            builder.Services
                .TryAddTransient<IIdentityDbContextAccessor, TDbContextAccessor>();

            return builder;
        }
    }
}
