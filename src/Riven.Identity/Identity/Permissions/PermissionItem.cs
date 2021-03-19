using JetBrains.Annotations;

using Riven.Authorization;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Riven.Identity.Permissions
{
    public class PermissionItem
    {
        /// <summary>
        /// hashcode
        /// </summary>
        protected virtual int HashCode { get; set; }

        /// <summary>
        /// 父级
        /// </summary>
        public virtual string Parent { get; protected set; }

        /// <summary>
        /// 名称
        /// </summary>
        public virtual string Name { get; protected set; }

        /// <summary>
        /// 范围
        /// </summary>
        public virtual PermissionAuthorizeScope Scope { get; protected set; }

        /// <summary>
        /// 排序号
        /// </summary>
        public virtual int Sort { get; protected set; }

        /// <summary>
        /// 是否禁用
        /// </summary>
        public virtual bool Disabled { get; set; }


        public PermissionItem([NotNull] string name, string parent = null, int? sort = null, PermissionAuthorizeScope scope = PermissionAuthorizeScope.Common)
        {
            Check.NotNull(name, nameof(name));

            this.Name = name;
            this.Parent = parent;
            this.Scope = scope;
            if (sort.HasValue)
            {
                this.Sort = sort.Value;
            }

            this.HashCode = this.Name.GetHashCode() + this.Scope.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return false;
            }

            if (obj is PermissionItem input)
            {
                return input.GetHashCode() == this.GetHashCode();
            }

            return false;
        }

        public override int GetHashCode()
        {
            return this.HashCode;
        }
    }

 
}
