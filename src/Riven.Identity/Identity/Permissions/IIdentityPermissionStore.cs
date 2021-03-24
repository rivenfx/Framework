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
        /// 创建权限
        /// </summary>
        /// <param name="permissions"></param>
        /// <returns></returns>
        Task CreateAsync([NotNull] IEnumerable<TPermission> permissions);

        /// <summary>
        /// 删除符合条件的权限
        /// </summary>
        /// <param name="type">类型</param>
        /// <param name="provider">映射</param>
        /// <param name="names">类型映射关联的数据</param>
        /// <returns></returns>
        Task Remove([NotNull] string type, [NotNull] string provider, params string[] names);

        /// <summary>
        /// 删除权限
        /// </summary>
        /// <param name="names">权限名称</param>
        /// <returns></returns>
        Task Remove(params string[] names);
    }
}
