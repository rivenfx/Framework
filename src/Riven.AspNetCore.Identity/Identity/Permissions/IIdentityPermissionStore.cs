using JetBrains.Annotations;

using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Riven.Identity.Permissions
{
    public interface IIdentityPermissionStore<TPermission>
        where TPermission : IdentityPermission
    {
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
        /// 查找权限
        /// </summary>
        /// <param name="type">权限类型.
        /// <see cref="IdentityPermission{TKey}.Type"/>
        /// </param>
        /// <param name="provider">类型映射关联的数据.</param>
        /// <returns></returns>
        Task<IEnumerable<string>> FindPermissions(string type, string provider);


        /// <summary>
        /// 删除权限
        /// </summary>
        /// <param name="names">权限名称</param>
        /// <returns></returns>
        Task Remove(params string[] names);
    }
}
