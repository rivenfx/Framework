using JetBrains.Annotations;

using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading.Tasks;

namespace Riven.Identity.Permissions
{
    public interface IIdentityPermissionStore<TPermission> : IIdentityPermissionFinder, IDisposable
        where TPermission : IdentityPermission
    {
        /// <summary>
        /// 查询器
        /// </summary>
        IQueryable<TPermission> Query { get; }

        /// <summary>
        /// 获取所有权限
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<string>> GetAll();

        /// <summary>
        /// 创建权限
        /// </summary>
        /// <param name="permission"></param>
        /// <returns></returns>
        Task CreateAsync([NotNull] TPermission permission);

        /// <summary>
        /// 删除权限
        /// </summary>
        /// <param name="names">权限名称</param>
        /// <returns></returns>
        Task Remove(params string[] names);
    }
}
