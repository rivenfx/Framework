using Riven.AspNetCore.Models;
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
        private WrapResultAttribute _defaultWrapResultAttribute;

        public RivenAspNetCoreOptions()
        {
            _defaultWrapResultAttribute = new WrapResultAttribute();
        }

        /// <summary>
        /// 默认的响应包装配置
        /// </summary>
        public virtual WrapResultAttribute DefaultWrapResultAttribute
        {
            get => this._defaultWrapResultAttribute;
            set
            {
                if (value != null)
                {
                    _defaultWrapResultAttribute = value;
                }
            }
        }

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
