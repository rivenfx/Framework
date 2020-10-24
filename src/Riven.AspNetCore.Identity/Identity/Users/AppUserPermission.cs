using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
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

        [NotMapped]
        [Obsolete("use the Name field")]
        public override string ClaimType { get; set; }

        [NotMapped]
        [Obsolete("use the Name field")]
        public override string ClaimValue { get; set; }

        /// <summary>
        /// permission name
        /// </summary>
        public virtual string Name { get; set; }
    }

    /// <summary>
    /// 
    /// </summary>
    public class AppUserPermission : AppUserPermission<long>
    {
    }
}
