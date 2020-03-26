using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Riven.Localization
{
    public class DefaultLocalizationHeaderRequestCultureProvider : RequestCultureProvider
    {
        /// <inheritdoc />
        public override Task<ProviderCultureResult> DetermineProviderCultureResult(HttpContext httpContext)
        {
            var cultureManager = httpContext.RequestServices.GetService<ICultureAccessor>();

            return cultureManager.GetHeaderRequestCulture(httpContext);
        }
    }
}
