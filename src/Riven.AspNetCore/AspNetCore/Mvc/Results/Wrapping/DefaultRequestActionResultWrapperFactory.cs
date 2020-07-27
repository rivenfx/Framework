using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Riven.AspNetCore.Mvc.Results.Wrapping
{
    public class DefaultRequestActionResultWrapperFactory : IRequestActionResultWrapperFactory
    {
        public virtual IRequestActionResultWrapper CreateFor(FilterContext context)
        {
            Check.NotNull(context, nameof(context));

            switch (context)
            {
                case ResultExecutingContext resultExecutingContext when resultExecutingContext.Result is ObjectResult:
                    return new ObjectActionResultWrapper();

                case ResultExecutingContext resultExecutingContext when resultExecutingContext.Result is JsonResult:
                    return new JsonActionResultWrapper();

                case ResultExecutingContext resultExecutingContext when resultExecutingContext.Result is EmptyResult:
                    return new EmptyActionResultWrapper();

                case PageHandlerExecutedContext pageHandlerExecutedContext when pageHandlerExecutedContext.Result is ObjectResult:
                    return new ObjectActionResultWrapper();

                case PageHandlerExecutedContext pageHandlerExecutedContext when pageHandlerExecutedContext.Result is JsonResult:
                    return new JsonActionResultWrapper();

                case PageHandlerExecutedContext pageHandlerExecutedContext when pageHandlerExecutedContext.Result is EmptyResult:
                    return new EmptyActionResultWrapper();

                default:
                    return new NoneActionResultWrapper();
            }
        }
    }
}
