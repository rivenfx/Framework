using Riven.Uow.Providers;

using System;
using System.Collections.Generic;
using System.Text;

namespace Riven.MultiTenancy
{
    public class UowMultiTenancyProvider : IMultiTenancyProvider
    {
        protected readonly ICurrentUnitOfWorkProvider _currentUnitOfWorkProvider;
        protected readonly IMultiTenancyOptions _multiTenancyOptions;

        public UowMultiTenancyProvider(ICurrentUnitOfWorkProvider currentUnitOfWorkProvider)
        {
            _currentUnitOfWorkProvider = currentUnitOfWorkProvider;
        }

        public virtual string CurrentTenantNameOrNull()
        {
            // 当前使用的连接字符串名称,为空则表示使用的默认的连接字符串名称
            var connectionStringName = this._currentUnitOfWorkProvider.Current
                .GetConnectionStringName();

            return connectionStringName;
        }
    }
}
