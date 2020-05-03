using Riven.AspNetCore.Mvc;
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
        /// 启用响应结果包装,默认为false,不启用
        /// </summary>
        public virtual bool ResultWrapEnable { get; set; }

        /// <summary>
        /// 异常处理完成事件
        /// </summary>
        public event EventHandler<Exception> OnHandledException;

        /// <summary>
        /// 触发异常处理完成事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="ex"></param>
        public void TriggerHandledException(object sender, Exception ex)
        {
            this.OnHandledException?.Invoke(sender, ex);
        }
    }
}
