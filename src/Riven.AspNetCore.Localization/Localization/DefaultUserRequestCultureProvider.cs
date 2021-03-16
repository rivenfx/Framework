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
using Microsoft.AspNetCore.Builder;

namespace Riven.Localization
{
    public class DefaultUserRequestCultureProvider : RequestCultureProvider
    {
        readonly RequestLocalizationOptions _requestLocalizationOptions;

        public DefaultUserRequestCultureProvider(RequestLocalizationOptions requestLocalizationOptions)
        {
            _requestLocalizationOptions = requestLocalizationOptions;
        }

        public override async Task<ProviderCultureResult> DetermineProviderCultureResult(HttpContext httpContext)
        {
            var cultureAccessor = httpContext.RequestServices.GetService<ICultureAccessor>();

            cultureAccessor.Options = this._requestLocalizationOptions;

            return await cultureAccessor.GetUserRequestCulture(httpContext);
        }
    }
}
