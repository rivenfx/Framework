using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Riven.AspNetCore.Mvc.Results.Wrapping
{
    public interface IRequestActionResultWrapperFactory
    {
        IRequestActionResultWrapper CreateFor(FilterContext context);
    }
}
