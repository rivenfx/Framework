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
        /// <summary>
        /// permission name
        /// </summary>
        public virtual string Name { get; set; }



        #region 重写 ClaimType  ClaimValue 并标记为过时字段

#pragma warning disable CS0809 // 过时成员重写未过时成员
        [NotMapped]
        [Obsolete("use the Name field", true)]
        public override string ClaimType { get; set; }

        [NotMapped]
        [Obsolete("use the Name field", true)]
        public override string ClaimValue { get; set; }
#pragma warning disable CS0809 // 过时成员重写未过时成员 

        #endregion


    }

    /// <summary>
    /// 
    /// </summary>
    public class AppUserPermission : AppUserPermission<long>
    {
    }
}
