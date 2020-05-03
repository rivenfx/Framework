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
        private string _requestActionInfoName = nameof(RequestActionInfo);

        /// <summary>
        /// 用于存储本次请求的 Action 信息的键值名称,默认值为 'RequestActionInfo'（存储在 HttpContext.Items 中)
        /// </summary>
        public virtual string RequestActionInfoName
        {
            get => this._requestActionInfoName;
            set => this._requestActionInfoName = value;
        }
      
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
