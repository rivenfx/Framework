using Microsoft.AspNetCore.Identity;

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Riven.Identity.Users
{
    /// <summary>
    /// 用户
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    public abstract class User<TKey> : IdentityUser<TKey>
        where TKey : IEquatable<TKey>
    {
        /// <summary>
        /// 昵称最大长度
        /// </summary>
        public const int NicknameMaxLength = 512;

        /// <summary>
        /// 昵称,不能为空
        /// </summary>
        [StringLength(NicknameMaxLength)]
        [Required]
        public virtual string Nickname { get; set; }

        /// <summary>
        /// 是否已激活
        /// </summary>
        public virtual bool IsActive { get; set; }

        /// <summary>
        /// 是否为系统内置
        /// </summary>
        public virtual bool IsStatic { get; set; }
    }
}
