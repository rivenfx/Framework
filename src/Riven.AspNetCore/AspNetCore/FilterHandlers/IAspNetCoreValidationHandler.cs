using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Riven.AspNetCore.FilterHandlers
{
    /// <summary>
    /// 参数验证处理器
    /// </summary>
    public interface IAspNetCoreValidationHandler
    {
        Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next);
    }
}
