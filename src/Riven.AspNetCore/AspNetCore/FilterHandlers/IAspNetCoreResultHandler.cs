using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Riven.AspNetCore.FilterHandlers
{
    /// <summary>
    /// 请求响应处理器
    /// </summary>
    public interface IAspNetCoreResultHandler
    {
        void OnResultExecuting(ResultExecutingContext context);

        void OnResultExecuted(ResultExecutedContext context);


        Task OnPageHandlerSelectionAsync(PageHandlerSelectedContext context);


        Task OnPageHandlerExecutionAsync(PageHandlerExecutingContext context, PageHandlerExecutionDelegate next);
    }
}
