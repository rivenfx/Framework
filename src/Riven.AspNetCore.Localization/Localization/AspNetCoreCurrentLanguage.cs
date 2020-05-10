using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;

namespace Riven.Localization
{
    public class AspNetCoreCurrentLanguage : ICurrentLanguage
    {
        public virtual string Culture => GetCurrentLanguage()?.Culture;

        protected readonly ILanguageManager _languageManager;
        protected readonly IHttpContextAccessor _httpContextAccessor;

        public AspNetCoreCurrentLanguage(ILanguageManager languageManager, IHttpContextAccessor httpContextAccessor)
        {
            _languageManager = languageManager;
            _httpContextAccessor = httpContextAccessor;
        }

        protected LanguageInfo GetCurrentLanguage()
        {
            var languages = _languageManager.GetEnabledLanguages();
            if (languages.Count <= 0)
            {
                throw new Exception("No language defined in this application.");
            }
            var currentCultureName = string.Empty;
            //var currentCultureName = CultureInfo.CurrentUICulture.Name;
            if (_httpContextAccessor != null && _httpContextAccessor.HttpContext != null)
            {
                var requestCultureFeature = _httpContextAccessor.HttpContext.Features.Get<IRequestCultureFeature>();
                var currentCultureName1 = requestCultureFeature.RequestCulture.UICulture.Name;
                var currentCultureName2 = requestCultureFeature.RequestCulture.Culture.Name;
            }
            else
            {
                currentCultureName = _languageManager.GetDefaultLanguage().Culture;
            }



            //Try to find exact match
            var currentLanguage = languages.FirstOrDefault(l => l.Culture == currentCultureName);
            if (currentLanguage != null)
            {
                return currentLanguage;
            }

            //Try to find best match
            currentLanguage = languages.FirstOrDefault(l => currentCultureName.StartsWith(l.Culture));
            if (currentLanguage != null)
            {
                return currentLanguage;
            }

            //Try to find default language
            currentLanguage = _languageManager.GetDefaultLanguage();
            if (currentLanguage != null)
            {
                return currentLanguage;
            }

            //Get first one
            return languages[0];
        }
    }
}
