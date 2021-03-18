using JetBrains.Annotations;

using Microsoft.EntityFrameworkCore;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Riven.Identity.Permissions
{
    public class IdentityPermissionStore<TPermission> : IIdentityPermissionStore<TPermission>
        where TPermission : IdentityPermission
    {
        public virtual DbContext Context => throw new NotImplementedException(nameof(Context));

        public virtual IQueryable<TPermission> Permissions => Context.Set<TPermission>();

        public async Task<IEnumerable<string>> GetAll()
        {
            return await Permissions.AsNoTracking()
                   .GroupBy(o => o.Name)
                   .Select(o => o.Key)
                   .ToListAsync();
        }

        public async Task CreateAsync([NotNull] TPermission permission)
        {
            Check.NotNull(permission, nameof(permission));

            await Context.AddAsync(permission);
        }

        public async Task<IEnumerable<string>> FindPermissions(string type, string provider)
        {
            return await Permissions.AsNoTracking()
                    .Where(o => o.Type == type && o.Provider == provider)
                    .Select(o => o.Name)
                    .ToListAsync();
        }

        public async Task Remove(params string[] names)
        {
            if (names == null || names.Length == 0)
            {
                return;
            }

            await Task.Yield();


            var permissions = await Permissions
                .Where(o => names.Contains(o.Name))
                .ToListAsync();

            this.Context.RemoveRange(permissions);
        }



        ///// <summary>
        ///// 校验权限
        ///// </summary>
        ///// <param name="name">
        ///// <see cref="TPermission.Name"/>
        ///// </param>
        ///// <param name="type">
        ///// <see cref="TPermission.Type"/>
        ///// </param>
        ///// <param name="provider">
        ///// <see cref="TPermission.Provider"/>
        ///// </param>
        ///// <returns></returns>
        //public async Task<bool> IsGrantedAsync(string name, string type, string provider)
        //{
        //    if (string.IsNullOrWhiteSpace(name))
        //    {
        //        return true;
        //    }


        //    var count = await Permissions.CountAsync(o => o.Name == name
        //            && o.Type == type
        //            && o.Provider == provider);

        //    return count > 0;
        //}

        ///// <summary>
        ///// 多个校验权限
        ///// </summary>
        ///// <param name="names">
        ///// <see cref="TPermission.Name"/>
        ///// </param>
        ///// <param name="type">
        ///// <see cref="TPermission.Type"/>
        ///// </param>
        ///// <param name="provider">
        ///// <see cref="TPermission.Provider"/>
        ///// </param>
        ///// <param name="requireAll">是否要校验所有权限</param>
        ///// <returns></returns>
        //public async Task<MultiPermissionGrantResult> IsGrantedAsync(string[] names, string type, string provider, bool requireAll = false)
        //{
        //    var result = new MultiPermissionGrantResult();

        //    if (names == null || names.Length == 0)
        //    {
        //        return MultiPermissionGrantResult.SuccessResult;
        //    }

        //    var successed = false;
        //    // 校验所有权限
        //    if (requireAll)
        //    {

        //        foreach (var item in names)
        //        {
        //            successed = await this.IsGrantedAsync(item, type, provider);

        //            if (!successed)
        //            {
        //                result.Unsuccessful.Add(item);
        //            }
        //        }

        //        result.SetSuccessed(!result.HasError);

        //        return result;
        //    }

        //    // 只校验部分权限
        //    foreach (var item in names)
        //    {
        //        successed = await this.IsGrantedAsync(item, type, provider);
        //        if (successed)
        //        {
        //            result.SetSuccessed(true);
        //            break;
        //        }
        //    }

        //    return result;
        //}


    }
}
