using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Riven.Identity.Users
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    public class AppUserPermission<TKey> : IdentityUserClaim<TKey>
     where TKey : IEquatable<TKey>
    {
    }

    /// <summary>
    /// 
    /// </summary>
    public class AppUserPermission : AppUserPermission<long>
    {
    }
}
