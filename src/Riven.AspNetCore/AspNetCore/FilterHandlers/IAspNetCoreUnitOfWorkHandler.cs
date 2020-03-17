using Microsoft.AspNetCore.Mvc.Filters;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Riven.AspNetCore.FilterHandlers
{
    /// <summary>
    /// 工作单元处理器
    /// </summary>
    public interface IAspNetCoreUnitOfWorkHandler
    {
        /// <summary>
        /// 工作单元filter
        /// </summary>
        /// <param name="context"></param>
        /// <param name="next"></param>
        /// <returns></returns>
        Task ActionFilterAsync(ActionExecutingContext context, ActionExecutionDelegate next);

        /// <summary>
        /// mvc工作单元filter
        /// </summary>
        /// <param name="context"></param>
        /// <param name="next"></param>
        /// <returns></returns>
        Task OnPageHandlerExecutionAsync(PageHandlerExecutingContext context, PageHandlerExecutionDelegate next);
    }
}
