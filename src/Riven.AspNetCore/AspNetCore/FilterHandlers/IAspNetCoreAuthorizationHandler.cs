using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Riven.AspNetCore.FilterHandlers
{
    /// <summary>
    /// 认证校验处理器
    /// </summary>
    public interface IAspNetCoreAuthorizationHandler
    {
        Task OnAuthorizationAsync(AuthorizationFilterContext context);
    }
}
