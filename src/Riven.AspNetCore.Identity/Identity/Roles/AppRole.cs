using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Riven.Identity.Roles
{
    /// <summary>
    /// 角色
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    public class AppRole<TKey> : IdentityRole<TKey>
        where TKey : IEquatable<TKey>
    {
        /// <summary>
        /// 唯一编码最大长度
        /// </summary>
        public const int CodeMaxLength = 512;

        /// <summary>
        /// 唯一编码
        /// </summary>
        [StringLength(CodeMaxLength)]
        public string Code { get; set; }
    }

    /// <summary>
    /// 
    /// </summary>
    public class AppRole : AppRole<long>
    {

    }

}
