using System.Collections.Generic;
using System.Threading.Tasks;

namespace Riven.Identity.Permissions
{
    /// <summary>
    /// 权限查找器
    /// </summary>
    public interface IIdentityPermissionFinder
    {
        /// <summary>
        /// 根据条件查找所有复合条件权限.
        /// </summary>
        /// <param name="type">权限类型.
        /// <see cref="IdentityPermission.Type"/>
        /// </param>
        /// <param name="provider">类型映射关联的数据.</param>
        /// <returns></returns>
        Task<IEnumerable<string>> FindPermissions(string type, string provider);

        /// <summary>
        /// 根据条件查找所有复合条件权限.
        /// </summary>
        /// <param name="type">权限类型.
        /// <see cref="IdentityPermission.Type"/>
        /// </param>
        /// <param name="providers">类型映射关联的数据.</param>
        /// <returns></returns>
        Task<IEnumerable<string>> FindPermissions(string type, string[] providers);

        /// <summary>
        /// 根据条件查找所有符合条件的权限.
        /// </summary>
        /// <param name="types">
        /// 权限类型.
        /// <see cref="IdentityPermission.Type"/>
        /// </param>
        /// <param name="providers">
        /// 类型映射关联的数据.
        /// </param>
        /// <returns></returns>
        Task<IEnumerable<string>> FindPermissions(string[] types, string[] providers);

        /// <summary>
        /// 根据条件判断是否存在某个权限.
        /// </summary>
        /// <param name="name">权限名称.</param>
        /// <param name="type">权限类型.
        /// <see cref="IdentityPermission.Type"/>
        /// </param>
        /// <param name="provider">类型映射关联的数据.</param>
        /// <returns></returns>
        Task<bool> ExistPermission(string name, string type, string provider);
    }
}
