using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Riven.AspNetCore.FilterHandlers
{
    /// <summary>
    /// 审计日志处理器
    /// </summary>
    public interface IAspNetCoreAuditHandler
    {

        /// <summary>
        /// API
        /// </summary>
        /// <param name="context"></param>
        /// <param name="next"></param>
        /// <returns></returns>
        Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next);


        /// <summary>
        /// MVC
        /// </summary>
        /// <param name="context"></param>
        /// <param name="next"></param>
        /// <returns></returns>
        Task OnPageHandlerExecutionAsync(PageHandlerExecutingContext context, PageHandlerExecutionDelegate next);
    }
}
