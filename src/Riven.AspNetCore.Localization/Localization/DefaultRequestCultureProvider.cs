﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Primitives;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

using Riven.Extensions;

namespace Riven.Localization
{
    public class DefaultRequestCultureProvider : RequestCultureProvider
    {
        public override async Task<ProviderCultureResult> DetermineProviderCultureResult(HttpContext httpContext)
        {
            var cultureManager = httpContext.RequestServices.GetService<ICultureAccessor>();

            return await cultureManager.GetDefaultRequestCulture(httpContext);
        }
    }
}
