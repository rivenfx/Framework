using System;

using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

using Riven.Authorization;
using Riven.Identity;
using Riven.Identity.Permissions;
using Riven.Identity.Users;
using Riven.Security;

namespace Riven
{
    public static class RivenIdentityServiceExtensions
    {
        /// <summary>
        /// 添加权限存储器
        /// </summary>
        /// <typeparam name="TPermissionStore"></typeparam>
        /// <param name="builder"></param>
        /// <returns></returns>
        public static IdentityBuilder AddPermissionStore<TPermissionStore>(this IdentityBuilder builder)
            where TPermissionStore : class
        {
            return builder.AddPermissionStore<TPermissionStore, PermissionItemStore>();
        }


        /// <summary>
        /// 添加权限存储器
        /// </summary>
        /// <typeparam name="TPermissionStore"></typeparam>
        /// <typeparam name="TPermissionItemStore">内存权限</typeparam>
        /// <param name="builder"></param>
        /// <returns></returns>
        public static IdentityBuilder AddPermissionStore<TPermissionStore, TPermissionItemStore>(this IdentityBuilder builder)
            where TPermissionStore : class
            where TPermissionItemStore : class, IPermissionItemStore
        {
            // 添加存储器
            var storeInterface = typeof(IIdentityPermissionStore<>).MakeGenericType(IdentityInfo.PermissionType);

            var storeImp = typeof(TPermissionStore);

            builder.Services.TryAddScoped(storeInterface, storeImp);


            if (storeImp.GetInterface(storeInterface.FullName) == null)
            {
                throw new Exception($"{storeImp.FullName} did not implement {storeInterface.FullName}");
            }



            builder.Services.TryAddScoped((serviceProvider) =>
            {
                var obj = serviceProvider.GetRequiredService(storeInterface);
                return (IIdentityPermissionFinder)obj;
            });

            builder.Services.TryAddSingleton<IPermissionItemStore, TPermissionItemStore>();

            return builder;
        }


        /// <summary>
        /// 添加用户角色查找器
        /// </summary>
        /// <typeparam name="TUserRoleFinder"></typeparam>
        /// <param name="builder"></param>
        /// <returns></returns>
        public static IdentityBuilder AddUserRoleFinder<TUserRoleFinder>(this IdentityBuilder builder)
            where TUserRoleFinder : IIdentityUserRoleFinder
        {
            builder.Services.TryAddScoped(
                typeof(IIdentityUserRoleFinder),
                typeof(TUserRoleFinder)
                );

            return builder;
        }

        /// <summary>
        /// 添加 Riven identity 服务
        /// </summary>
        /// <param name="builder"></param>
        /// <returns></returns>
        public static IdentityBuilder AddRivenIdentityCore(this IdentityBuilder builder)
        {
            builder.Services.AddRivenSecurity();

            builder.Services.TryAddTransient<ICurrentUser, CurrentUser>();

            builder.Services.TryAddTransient<IPermissionChecker, PermissionChecker>();

            return builder;
        }

        /// <summary>
        /// 添加权限初始化器
        /// </summary>
        /// <typeparam name="TPermissionInitializer"></typeparam>
        /// <param name="builder"></param>
        /// <returns></returns>
        public static IdentityBuilder AddPermissionInitializer<TPermissionInitializer>(this IdentityBuilder builder)
            where TPermissionInitializer : class, IPermissionInitializer
        {
            builder.Services
                .AddSingleton<IPermissionInitializer, TPermissionInitializer>();

            return builder;
        }
    }
}