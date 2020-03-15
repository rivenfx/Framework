using System;
using System.Collections.Generic;
using System.Text;
using System.Transactions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Riven.Uow.Handles;
using Riven.Uow.Providers;

namespace Riven.Uow
{
    /// <summary>
    /// 默认实现的工作单元管理器
    /// </summary>
    public class DefaultUnitOfWorkManager : IUnitOfWorkManager
    {
        protected readonly IServiceProvider _serviceProvider;
        protected readonly ICurrentUnitOfWorkProvider _currentUnitOfWorkProvider;

        public IActiveUnitOfWork Current => _currentUnitOfWorkProvider.Current;

        public DefaultUnitOfWorkManager(
            IServiceProvider serviceProvider,
             ICurrentUnitOfWorkProvider currentUnitOfWorkProvider
            )
        {
            _serviceProvider = serviceProvider;
            _currentUnitOfWorkProvider = currentUnitOfWorkProvider;
        }

        public IUnitOfWorkCompleteHandle Begin()
        {
            return Begin(new UnitOfWorkOptions());
        }

        public IUnitOfWorkCompleteHandle Begin(TransactionScopeOption scope)
        {
            return Begin(new UnitOfWorkOptions { Scope = scope });
        }

        public IUnitOfWorkCompleteHandle Begin(UnitOfWorkOptions options)
        {
            var outerUow = _currentUnitOfWorkProvider.Current;

            if (options.Scope == TransactionScopeOption.Required && outerUow != null)
            {
                return new InnerUnitOfWorkCompleteHandle();
            }

            var uow = _serviceProvider.GetService<IUnitOfWork>();

            uow.Completed += (sender, args) =>
            {
                _currentUnitOfWorkProvider.Current = null;
            };

            uow.Failed += (sender, args) =>
            {
                _currentUnitOfWorkProvider.Current = null;
            };

            uow.Disposed += (sender, args) =>
            {
                uow.Dispose();
            };

            uow.Begin(options);

            // Inherit connectionStringName from outer UOW
            if (outerUow != null)
            {
                uow.SetConnectionStringName(outerUow.GetConnectionStringName());
            }

            _currentUnitOfWorkProvider.Current = uow;

            return uow;
        }
    }
}
