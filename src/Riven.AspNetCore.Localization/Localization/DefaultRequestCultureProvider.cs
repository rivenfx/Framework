using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Primitives;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Riven.Localization
{
    public class DefaultRequestCultureProvider : RequestCultureProvider
    {
        public override Task<ProviderCultureResult> DetermineProviderCultureResult(HttpContext httpContext)
        {
            var languageManager = httpContext.RequestServices.GetService<ILanguageManager>();
            var defaultLanguage = languageManager.DefaultLanguage;
            //new StringSegment()
            //return new ProviderCultureResult();
            throw new NotImplementedException();
        }
    }
}
