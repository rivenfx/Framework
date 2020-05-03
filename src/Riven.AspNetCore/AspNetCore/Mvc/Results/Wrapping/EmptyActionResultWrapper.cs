using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Riven.AspNetCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Riven.AspNetCore.Mvc.Results.Wrapping
{
    public class EmptyActionResultWrapper : IRequestActionResultWrapper
    {
        public void Wrap(FilterContext context)
        {
            switch (context)
            {
                case ResultExecutingContext resultExecutingContext:
                    resultExecutingContext.Result = new ObjectResult(new AjaxResponse());
                    return;

                case PageHandlerExecutedContext pageHandlerExecutedContext:
                    pageHandlerExecutedContext.Result = new ObjectResult(new AjaxResponse());
                    return;
            }

           
        }
    }
}
