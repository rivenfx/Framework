using Riven.Authorization;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System.Threading.Tasks;
using JetBrains.Annotations;

namespace Riven.Identity.Permissions
{
    public interface IPermissionManager<TPermission>
        where TPermission : IdentityPermission
    {

        /// <summary>
        /// 获取系统中的权限项
        /// </summary>
        /// <returns></returns>
        IQueryable<PermissionItem> GetSystemItem();


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
        Task<IEnumerable<string>> FindPermissions(string type, IEnumerable<string> providers);

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
        Task<IEnumerable<string>> FindPermissions(IEnumerable<string> types, IEnumerable<string> providers);

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

        /// <summary>
        /// 删除符合条件的权限
        /// </summary>
        /// <param name="type">类型</param>
        /// <param name="provider">映射</param>
        /// <param name="names">类型映射关联的数据</param>
        /// <returns></returns>
        Task Remove([NotNull] string type, string provider, params string[] names);
    }

    public class PermissionManager<TPermission> : IPermissionManager<TPermission>
        where TPermission : IdentityPermission
    {
        protected readonly IIdentityPermissionStore<TPermission> _store;
        protected readonly IPermissionItemStore _itemStore;

        public PermissionManager(IServiceProvider serviceProvider)
        {
            _store = serviceProvider
                .GetService<IIdentityPermissionStore<TPermission>>();
            _itemStore = serviceProvider.GetService<IPermissionItemStore>();

        }

        protected virtual IQueryable<PermissionItem> ItemQuery => _itemStore.Query;

        protected virtual IIdentityPermissionStore<TPermission> Store => _store;

        public virtual IQueryable<PermissionItem> GetSystemItem()
        {
            return _itemStore.Query;
        }


        public virtual Task CreateAsync([NotNull] TPermission permission)
        {
            return this.Store.CreateAsync(permission);
        }

        public virtual Task<bool> ExistPermission(string name, string type, string provider)
        {
            return this.Store.ExistPermission(name, type, provider);
        }

        public virtual Task<IEnumerable<string>> FindPermissions(string type, string provider)
        {
            return this.Store.FindPermissions(type, provider);
        }

        public virtual Task<IEnumerable<string>> FindPermissions(string type, IEnumerable<string> providers)
        {
            return this.Store.FindPermissions(type, providers);
        }

        public virtual Task<IEnumerable<string>> FindPermissions(IEnumerable<string> types, IEnumerable<string> providers)
        {
            return this.Store.FindPermissions(types, providers);
        }


        public virtual Task Remove(params string[] names)
        {
            return this.Store.Remove(names);
        }

        public virtual Task Remove([NotNull] string type, string provider, params string[] names)
        {
            return this.Store.Remove(type, provider, names);
        }


    }
}
