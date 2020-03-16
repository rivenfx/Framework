using Microsoft.AspNetCore.Mvc.Filters;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Riven.AspNetCore.FilterHandlers
{
    /// <summary>
    /// 异常处理器
    /// </summary>
    public interface IAspNetCoreExceptionHandeler
    {
        /// <summary>
        /// 异常处理filter
        /// </summary>
        /// <param name="context"></param>
        void OnException(ExceptionContext context);

        /// <summary>
        /// mvc异常处理filter
        /// </summary>
        /// <param name="context"></param>
        /// <param name="next"></param>
        /// <returns></returns>
        Task OnExceptionPageHandlerExecutionAsync(PageHandlerExecutingContext context, PageHandlerExecutionDelegate next);

    }
}
