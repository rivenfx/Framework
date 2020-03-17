using Microsoft.AspNetCore.Builder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Riven.AspNetCore.Accessors
{
    public interface IApplicationBuilderAccessor
    {
        IApplicationBuilder ApplicationBuilder { get; set; }
    }
}
