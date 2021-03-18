using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace Riven.Identity.Permissions
{
    /// <summary>
    /// 应用权限
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    public class AppPermission<TKey>
         where TKey : IEquatable<TKey>
    {
        /// <summary>
        /// <see cref="Name"/> 最大长度
        /// </summary>
        public const int NameMaxLength = 128;

        /// <summary>
        /// <see cref="Type"/> 最大长度
        /// </summary>
        public const int TypeMaxLength = 16;

        /// <summary>
        /// <see cref="Provider"/> 最大长度
        /// </summary>
        public const int ProviderMaxLength = 64;


        /// <summary>
        /// Id
        /// </summary>
        public virtual TKey Id { get; set; }

        /// <summary>
        /// 权限名称
        /// </summary>
        [Required]
        [StringLength(NameMaxLength)]
        public string Name { get; set; }

        /// <summary>
        /// 权限类型
        /// </summary>
        [Required]
        [StringLength(TypeMaxLength)]
        public string Type { get; set; }

        /// <summary>
        /// 权限类型对应的数据映射
        /// (如:用户名称/角色名称)
        /// </summary>
        [Required]
        [StringLength(ProviderMaxLength)]
        public string Provider { get; set; }
    }


    public class AppPermission : AppPermission<Guid>
    {

    }
}
