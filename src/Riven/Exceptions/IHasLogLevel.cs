using Microsoft.Extensions.Logging;

using System;
using System.Collections.Generic;
using System.Text;

namespace Riven.Exceptions
{
    public interface IHasLogLevel
    {
        /// <summary>
        /// 日志级别
        /// </summary>
        public LogLevel LogLevel { get; set; }
    }
}
