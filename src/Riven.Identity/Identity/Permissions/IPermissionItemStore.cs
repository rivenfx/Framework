
using Riven.Authorization;

using System;
using System.Collections.Generic;
using System.Linq;


namespace Riven.Identity.Permissions
{
    public interface IPermissionItemStore
    {
        /// <summary>
        /// 查询器
        /// </summary>
        IQueryable<PermissionItem> Query { get; }

    }

    public class PermissionItemStore : IPermissionItemStore
    {
        protected readonly IPermissionInitializer _permissionInitializer;
        protected readonly Lazy<Dictionary<PermissionItem, int>> _permissions;


        public PermissionItemStore(IPermissionInitializer permissionInitializer)
        {
            _permissionInitializer = permissionInitializer;

            _permissions = new Lazy<Dictionary<PermissionItem, int>>(() =>
            {
                return _permissionInitializer.Run().ToDictionary(o => o, o => 0);
            });
        }

        public IQueryable<PermissionItem> Query => _permissions.Value.Keys
            .AsQueryable();
    }
}
