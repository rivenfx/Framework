using Microsoft.AspNetCore.Mvc.Filters;
using Riven.AspNetCore.FilterHandlers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Riven.AspNetCore.Mvc.ExceptionHandling
{
    public class AppExceptionFilter : IExceptionFilter, IAsyncPageFilter
    {
        protected readonly IAspNetCoreExceptionHandeler _aspNetCoreExceptionHandeler;

        public AppExceptionFilter(IAspNetCoreExceptionHandeler aspNetCoreUnitOfWorkHandler)
        {
            _aspNetCoreExceptionHandeler = aspNetCoreUnitOfWorkHandler;
        }


        public Task OnPageHandlerSelectionAsync(PageHandlerSelectedContext context)
        {
            return Task.CompletedTask;
        }


        public void OnException(ExceptionContext context)
        {
            this._aspNetCoreExceptionHandeler.OnException(context);
        }


        public Task OnPageHandlerExecutionAsync(PageHandlerExecutingContext context, PageHandlerExecutionDelegate next)
        {
            return _aspNetCoreExceptionHandeler.OnExceptionPageHandlerExecutionAsync(context, next);
        }

    }
}
