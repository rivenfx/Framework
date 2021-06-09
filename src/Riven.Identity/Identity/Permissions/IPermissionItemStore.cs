
using Riven.Authorization;

using System.Linq;


namespace Riven.Identity.Permissions
{
    /// <summary>
    /// 权限项存储器
    /// </summary>
    public interface IPermissionItemStore
    {
        /// <summary>
        /// 查询器
        /// </summary>
        IQueryable<PermissionItem> Query { get; }

    }
}
