using System;

using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

using Riven.Authorization;
using Riven.Identity;
using Riven.Identity.Permissions;
using Riven.Identity.Roles;
using Riven.Identity.Users;
using Riven.Security;

namespace Riven
{
    public static class RivenIdentityServiceExtensions
    {
        /// <summary>
        /// 添加 Riven identity 核心服务
        /// </summary>
        /// <param name="builder"></param>
        /// <returns></returns>
        public static IdentityBuilder AddRivenIdentityCore<TPermission>(this IdentityBuilder builder)
             where TPermission : IdentityPermission
        {
            IdentityInfo.PermissionType = typeof(TPermission);

            builder.Services.AddRivenSecurity();

            builder.Services.TryAddTransient<ICurrentUser, CurrentUser>();

            builder.Services.TryAddTransient<IPermissionChecker, PermissionChecker>();

            return builder;
        }


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
            // 权限存储器
            var storeInterface = typeof(IIdentityPermissionStore<>).MakeGenericType(IdentityInfo.PermissionType);

            var storeImp = typeof(TPermissionStore);
            if (storeImp.GetInterface(storeInterface.Name) == null)
            {
                throw new Exception($"{storeImp.FullName} did not implement {storeInterface.FullName}");
            }
            builder.Services.TryAddScoped(storeInterface, storeImp);
            builder.Services.TryAddScoped((provider) =>
            {
                return (TPermissionStore)provider.GetService(storeInterface);
            });


            // 权限查找器
            builder.Services.TryAddScoped((serviceProvider) =>
            {
                var obj = serviceProvider.GetRequiredService(storeInterface);
                return (IIdentityPermissionFinder)obj;
            });

            // 系统权限存储器
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
        /// 添加权限初始化器
        /// </summary>
        /// <typeparam name="TPermissionInitializer"></typeparam>
        /// <param name="builder"></param>
        /// <returns></returns>
        public static IdentityBuilder AddPermissionInitializer<TPermissionInitializer>(this IdentityBuilder builder)
            where TPermissionInitializer : class, IPermissionInitializer
        {
            builder.Services
                .TryAddSingleton<IPermissionInitializer, TPermissionInitializer>();

            return builder;
        }

        /// <summary>
        /// 添加权限管理器
        /// </summary>
        /// <typeparam name="TPermissionManager"></typeparam>
        /// <param name="builder"></param>
        /// <returns></returns>
        public static IdentityBuilder AddPermissionManager<TPermissionManager>(this IdentityBuilder builder)
            where TPermissionManager : class
        {
            // 添加存储器
            var managerType = typeof(IPermissionManager<>).MakeGenericType(IdentityInfo.PermissionType);

            builder.Services
                .TryAddScoped(managerType, typeof(TPermissionManager));
            builder.Services
                .TryAddScoped((provider) =>
                {
                    return (TPermissionManager)provider.GetService(managerType);
                });

            return builder;
        }


        /// <summary>
        /// 添加 Rievn 实现的 UserClaimsPrincipalFactory
        /// </summary>
        /// <typeparam name="TUser"></typeparam>
        /// <typeparam name="TRole"></typeparam>
        /// <typeparam name="TKey"></typeparam>
        /// <param name="builder"></param>
        /// <returns></returns>
        public static IdentityBuilder AddRivenClaimsPrincipalFactory<TUser, TRole, TKey>(this IdentityBuilder builder)
            where TUser : User<TKey>
            where TRole : Role<TKey>
            where TKey : IEquatable<TKey>
        {
            return builder.AddClaimsPrincipalFactory<
                IdentityUserClaimsPrincipalFactory<TUser, TRole, TKey>
                >();
        }
    }
}
