using System;
using System.Collections.Generic;
using System.Text;

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
        /// Id
        /// </summary>
        public virtual TKey Id { get; set; }

        /// <summary>
        /// 权限名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 权限类型
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// 权限类型对应的数据映射
        /// (如:用户名称/角色名称)
        /// </summary>
        public string Provider { get; set; }
    }


    public class AppPermission : AppPermission<Guid>
    {

    }
}
