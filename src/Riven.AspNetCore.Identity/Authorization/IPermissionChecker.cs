using Microsoft.AspNetCore.Authorization;
using Riven.Authorization;
using Riven.Identity.Authorization;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using JetBrains.Annotations;

namespace Riven.Authorization
{
    public interface IPermissionChecker
    {
        /// <summary>
        /// 当前登录用户是否拥有此权限
        /// </summary>
        /// <param name="permission"></param>
        /// <returns></returns>
        bool IsGranted([NotNull] string permission);

        /// <summary>
        /// 当前登录用户是否拥有此权限
        /// </summary>
        /// <param name="permission"></param>
        /// <returns></returns>
        Task<bool> IsGrantedAsync([NotNull] string permission);

        /// <summary>
        /// 当前登录用户是否拥有以下权限
        /// </summary>
        /// <param name="requireAll"></param>
        /// <param name="permissions"></param>
        /// <returns></returns>
        bool IsGranted(bool requireAll, params string[] permissions);

        /// <summary>
        /// 当前登录用户是否拥有以下权限
        /// </summary>
        /// <param name="requireAll"></param>
        /// <param name="permissions"></param>
        /// <returns></returns>
        Task<bool> IsGrantedAsync(bool requireAll, params string[] permissions);


        /// <summary>
        /// 指定用户是否拥有以下权限
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="permission"></param>
        /// <returns></returns>
        bool IsGranted([NotNull] string userId, [NotNull] string permission);

        /// <summary>
        /// 指定用户是否拥有以下权限
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="permission"></param>
        /// <returns></returns>
        Task<bool> IsGrantedAsync([NotNull] string userId, [NotNull] string permission);

        /// <summary>
        /// 指定用户是否拥有以下权限
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="requireAll"></param>
        /// <param name="permissions"></param>
        /// <returns></returns>
        bool IsGranted([NotNull] string userId, bool requireAll, params string[] permissions);

        /// <summary>
        /// 指定用户是否拥有以下权限
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="requireAll"></param>
        /// <param name="permissions"></param>
        /// <returns></returns>
        Task<bool> IsGrantedAsync([NotNull] string userId, bool requireAll, params string[] permissions);
    }
}
