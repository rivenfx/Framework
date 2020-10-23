using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Riven.Identity.Roles
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    public class AppRolePermission<TKey> : IdentityRoleClaim<TKey>
        where TKey : IEquatable<TKey>
    {

    }

    /// <summary>
    /// 
    /// </summary>
    public class AppRolePermission : AppRolePermission<long>
    {

    }

}
