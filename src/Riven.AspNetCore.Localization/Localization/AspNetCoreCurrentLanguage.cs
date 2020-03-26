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
        public string Culture => GetCurrentLanguage()?.Culture;

        protected readonly ILanguageManager _languageManager;

        public AspNetCoreCurrentLanguage(ILanguageManager languageManager)
        {
            _languageManager = languageManager;
        }

        protected LanguageInfo GetCurrentLanguage()
        {
            var languages = _languageManager.GetEnabledLanguages();
            if (languages.Count <= 0)
            {
                throw new Exception("No language defined in this application.");
            }

            var currentCultureName = CultureInfo.CurrentUICulture.Name;

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
