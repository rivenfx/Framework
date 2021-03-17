using JetBrains.Annotations;

using Riven.Uow;

using System;
using System.Collections.Generic;
using System.Text;

namespace Riven.Extensions
{
    public static class UowMultiTenancyExtensions
    {
        /// <summary>
        /// 切换租户
        /// </summary>
        /// <param name="uow">工作单元</param>
        /// <param name="tenantName">租户名称</param>
        /// <returns></returns>
        public static IDisposable ChangeTenant([NotNull] this IActiveUnitOfWork uow, string tenantName)
        {
            Check.NotNull(uow, nameof(uow));

            return uow.SetConnectionStringName(tenantName);
        }
    }
}
