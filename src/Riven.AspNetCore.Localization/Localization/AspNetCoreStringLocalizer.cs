using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace Riven.Localization
{
    public class AspNetCoreStringLocalizer : IStringLocalizer
    {
        public LocalizedString this[string name] => this.LocalizedString(name);

        public LocalizedString this[string name, params object[] arguments] => this.LocalizedString(name, arguments);

        readonly ILocalizationManager _localizationManager;

        readonly CultureInfo _cultureInfo;

        public AspNetCoreStringLocalizer(ILocalizationManager localizationManager, CultureInfo culture = null)
        {
            _localizationManager = localizationManager;
            _cultureInfo = culture;
        }


        public IEnumerable<LocalizedString> GetAllStrings(bool includeParentCultures)
        {
            LanguageInfo languageInfo = null;
            if (_cultureInfo == null)
            {
                languageInfo = this._localizationManager.LanguageManager.GetEnabledLanguages()
                    .FirstOrDefault(o => o.Culture == this._localizationManager.CurrentLanguage);

            }
            else
            {
                languageInfo = this._localizationManager.LanguageManager.GetEnabledLanguages()
                  .FirstOrDefault(o => o.Culture == _cultureInfo.Name);
            }

            return languageInfo.Texts.Select(o =>
             {
                 return new LocalizedString(o.Key, o.Value);
             });
        }

        public IStringLocalizer WithCulture(CultureInfo culture)
        {
            return new AspNetCoreStringLocalizer(this._localizationManager, culture);
        }

        private LocalizedString LocalizedString(string name, params object[] arguments)
        {
            var val = string.Empty;

            if (_cultureInfo == null)
            {
                val = _localizationManager.L(name, arguments);
            }
            else
            {
                val = _localizationManager.L(_cultureInfo.Name, name, arguments);
            }
            return new LocalizedString(name, val);
        }
    }
}
