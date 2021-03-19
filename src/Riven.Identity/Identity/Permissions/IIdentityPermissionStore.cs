using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using JetBrains.Annotations;

namespace Riven.Identity.Permissions
{
    /// <summary>
    /// 权限存储器
    /// </summary>
    /// <typeparam name="TPermission"></typeparam>
    public interface IIdentityPermissionStore<TPermission> : IIdentityPermissionFinder
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
