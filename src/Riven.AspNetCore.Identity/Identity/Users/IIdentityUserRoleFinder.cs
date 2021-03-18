using JetBrains.Annotations;

using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Riven.Identity.Users
{
    /// <summary>
    /// 用户角色查找器
    /// </summary>
    public interface IIdentityUserRoleFinder
    {
        /// <summary>
        /// 根据用户id获取角色
        /// </summary>
        /// <param name="userId">用户id</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<IEnumerable<string>> GetRolesByUserIdAsync([NotNull] string userId, CancellationToken cancellationToken = default(CancellationToken));
    }
}
