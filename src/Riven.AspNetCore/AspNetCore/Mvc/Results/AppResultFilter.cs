using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Riven.AspNetCore.Mvc.Results
{
    public class AppResultFilter : IResultFilter, IAsyncPageFilter
    {
        public Task OnPageHandlerExecutionAsync(PageHandlerExecutingContext context, PageHandlerExecutionDelegate next)
        {
            throw new NotImplementedException();
        }

        public Task OnPageHandlerSelectionAsync(PageHandlerSelectedContext context)
        {
            throw new NotImplementedException();
        }

        public void OnResultExecuted(ResultExecutedContext context)
        {
            throw new NotImplementedException();
        }

        public void OnResultExecuting(ResultExecutingContext context)
        {
            throw new NotImplementedException();
        }
    }
}
