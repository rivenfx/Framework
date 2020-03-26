using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using JetBrains.Annotations;
using System.Threading.Tasks;

namespace Riven.Localization
{
    public class DefaultLanguageManager : ILanguageManager
    {
        protected static Dictionary<string, LanguageInfo> _data = new Dictionary<string, LanguageInfo>();



        private LanguageInfo _defaultLanguage;

        public LanguageInfo DefaultLanguage => _defaultLanguage ?? this.GetEnabledLanguages().FirstOrDefault();

        protected Dictionary<string, LanguageInfo> Data => _data;

        public void Add([NotNull]LanguageInfo language)
        {
            Data[language.Name] = language;
        }

        public void AddRange([NotNull]List<LanguageInfo> languages)
        {
            languages.AddRange(languages);
        }

        public void ChangeDefaultLanguage(string languageName)
        {
            throw new NotImplementedException();
        }

        public void Clear()
        {
            Data.Clear();
        }

        public IReadOnlyList<LanguageInfo> GetAllLanguages()
        {
            return Data.Values
                    .ToList()
                    .AsReadOnly();
        }

        public LanguageInfo GetDefaultLanguage()
        {
            return this._defaultLanguage ?? this.GetEnabledLanguages().FirstOrDefault();
        }

        public Task<LanguageInfo> GetDefaultLanguageAsync()
        {
            return Task.FromResult(this.GetDefaultLanguage());
        }

        public IReadOnlyList<LanguageInfo> GetEnabledLanguages()
        {
            return Data.Values
                    .Where(o => o.Enabled)
                    .ToList()
                    .AsReadOnly();
        }

        public void Remove([NotNull]string languageName)
        {
            if (Data.Any(o => o.Key == languageName))
            {
                Data.Remove(languageName);
            }
        }
    }
}
