using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Riven.Configuration
{
    /// <summary>
    /// 配置项
    /// </summary>
    public class RivenAspNetCoreOptions
    {
        /// <summary>
        /// 启用认证Filter,默认为false,不启用
        /// </summary>
        public virtual bool AuthorizationFilterEnable { get; set; }

        /// <summary>
        /// 启用审计Filter,默认为false,不启用
        /// </summary>
        public virtual bool AuditFilterEnable { get; set; }

        /// <summary>
        /// 启用校验Filter,默认为false,不启用
        /// </summary>
        public virtual bool ValidationFilterEnable { get; set; }

        /// <summary>
        /// 启用工作单元Filter,默认为false,不启用
        /// </summary>
        public virtual bool UnitOfWorkFilterEnable { get; set; }

        /// <summary>
        /// 启用异常处理Filter,默认为false,不启用
        /// </summary>
        public virtual bool ExceptionFilterEnable { get; set; }

        /// <summary>
        /// 启用响应结果处理Filter,默认为false,不启用
        /// </summary>
        public virtual bool ResultFilterEnable { get; set; }

    }
}
