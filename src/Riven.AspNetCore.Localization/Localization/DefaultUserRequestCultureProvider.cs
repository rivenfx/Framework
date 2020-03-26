using JetBrains.Annotations;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Riven.Extensions;

namespace Riven.Localization
{
    public class DefaultUserRequestCultureProvider : RequestCultureProvider
    {

        public override async Task<ProviderCultureResult> DetermineProviderCultureResult(HttpContext httpContext)
        {
            var cultureManager = httpContext.RequestServices.GetService<ICultureAccessor>();

            return await cultureManager.GetUserRequestCulture(httpContext);
        }
    }
}
