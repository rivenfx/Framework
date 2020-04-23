using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Riven
{
    public static class RivenIdentityExtensions
    {
        public static IdentityBuilder AddRivenIdentity<TUser, TRole, TUserManager, TRoleManager, TUserStore, TRoleStore, TSignInManager>(this IServiceCollection services, Action<IdentityOptions> setupAction = null)
            where TUser : class
            where TRole : class
            where TUserManager : UserManager<TUser>
            where TRoleManager : RoleManager<TRole>
            where TUserStore : class, IUserStore<TUser>
            where TRoleStore : class, IRoleStore<TRole>
            where TSignInManager : SignInManager<TUser>
        {
            var identityBuilder = services.AddIdentity<TUser, TRole>(setupAction ?? null);
            identityBuilder
                .AddUserManager<TUserManager>()
                .AddRoleManager<TRoleManager>()
                .AddUserStore<TUserStore>()
                .AddRoleStore<TRoleStore>()
                .AddSignInManager<TSignInManager>();


            return identityBuilder;
        }
    }
}
