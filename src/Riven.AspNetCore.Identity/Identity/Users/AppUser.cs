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
    public abstract class AppUser<TKey> : IdentityUser<TKey>
     where TKey : IEquatable<TKey>
    {
        /// <summary>
        /// 唯一编码最大长度
        /// </summary>
        public const int CodeMaxLength = 512;

        /// <summary>
        /// 昵称最大长度
        /// </summary>
        public const int NicknameMaxLength = 512;

        /// <summary>
        /// 唯一编码
        /// </summary>
        [StringLength(CodeMaxLength)]
        public virtual string Code { get; set; }

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
    }

    /// <summary>
    /// 用户,主键类型为long
    /// </summary>
    public class AppUser : AppUser<long>
    {

    }
}
