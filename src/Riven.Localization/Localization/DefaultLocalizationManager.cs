using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using JetBrains.Annotations;

namespace Riven.Localization
{
    public class DefaultLocalizationManager : ILocalizationManager
    {
        protected readonly ILanguageManager _languageManager;
        protected readonly ICurrentLanguage _currentLanguage;

        public IQueryable<LanguageInfo> Languages
        {
            get
            {
                return _languageManager.GetAllLanguages().AsQueryable();
            }
        }

        public string CurrentLanguage => _currentLanguage.Culture;

        public ILanguageManager LanguageManager => _languageManager;

        public DefaultLocalizationManager(ILanguageManager languageManager, ICurrentLanguage currentLanguage)
        {
            _languageManager = languageManager;
            _currentLanguage = currentLanguage;
        }


        public string L([NotNull]string languageName, [NotNull]string languageKey, params object[] args)
        {
            var languageInfo = Languages.FirstOrDefault(o => o.Culture == languageName);
            if (languageInfo == null)
            {
                throw new Exception($"未注册此语言: {languageName}");
            }
            if (!languageInfo.Texts.TryGetValue(languageKey, out string languageText))
            {
                return languageKey;
            }

            return string.Format(languageText, args);
        }

        public string L([NotNull]string languageKey, params object[] args)
        {
            return this.L(_currentLanguage.Culture, languageKey, args);
        }
    }
}
