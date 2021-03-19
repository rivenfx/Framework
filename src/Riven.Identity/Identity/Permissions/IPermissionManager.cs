using Riven.Authorization;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Riven.Identity.Permissions
{
    public interface IPermissionManager<TPermission>
        where TPermission : IdentityPermission
    {
        /// <summary>
        /// 初始化
        /// </summary>
        void Init();

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="fromCache">从缓存</param>
        /// <returns></returns>
        IQueryable<PermissionItem> GetAll(bool fromCache = false);
    }

    public class PermissionManager
    {
        protected readonly IPermissionInitializer _permissionInitializer;
        protected readonly Lazy<Dictionary<PermissionItem, int>> _permissions;


        public PermissionManager()
        {
            _permissions = new Lazy<Dictionary<PermissionItem, int>>(() =>
              {
                  return _permissionInitializer.Run().ToDictionary(o => o, o => 0);
              });
        }




    }
}
