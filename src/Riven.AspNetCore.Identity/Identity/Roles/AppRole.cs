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
        /// 显示名称最大长度
        /// </summary>
        public const int DisplayNameMaxLength = 512;

        /// <summary>
        /// 描述最大长度
        /// </summary>
        public const int DescriptionMaxLength = 1024;

        /// <summary>
        /// 显示名称
        /// </summary>
        [StringLength(DisplayNameMaxLength)]
        public string DispayName { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        [StringLength(DescriptionMaxLength)]
        public string Description { get; set; }
    }

    /// <summary>
    /// 
    /// </summary>
    public class AppRole : AppRole<long>
    {

    }

}
