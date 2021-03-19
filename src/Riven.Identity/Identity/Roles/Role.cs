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
    public abstract class Role<TKey> : IdentityRole<TKey>
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
        public virtual string DisplayName { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        [StringLength(DescriptionMaxLength)]
        public virtual string Description { get; set; }

        /// <summary>
        /// 是否为系统内置
        /// </summary>
        public virtual bool IsStatic { get; set; }
    }
}
