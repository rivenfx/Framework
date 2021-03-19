using Riven.Authorization;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Riven.Identity.Permissions
{
    public interface IPermissionManager<TPermission>
        where TPermission : IdentityPermission
    {
        IIdentityPermissionStore<TPermission> Store { get; }

        IQueryable<PermissionItem> ItemQuery { get; }
    }

    public class PermissionManager<TPermission> : IPermissionManager<TPermission>
        where TPermission : IdentityPermission
    {
        protected readonly IIdentityPermissionStore<TPermission> _store;
        protected readonly IPermissionItemStore _itemStore;



        public PermissionManager(IServiceProvider serviceProvider)
        {
            _store = serviceProvider
                .GetService<IIdentityPermissionStore<TPermission>>();
            _itemStore = serviceProvider.GetService<IPermissionItemStore>();

        }

        public virtual IQueryable<PermissionItem> ItemQuery => _itemStore.Query;

        public virtual IIdentityPermissionStore<TPermission> Store => _store;
    }
}
