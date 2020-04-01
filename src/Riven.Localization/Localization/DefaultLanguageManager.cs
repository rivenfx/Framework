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

        protected static LanguageInfo _defaultLanguage = null;

        protected Dictionary<string, LanguageInfo> Data => _data;

        public void AddOrUpdate([NotNull]LanguageInfo language)
        {
            Data[language.Culture] = language;
        }

        public void AddOrUpdateRange([NotNull]List<LanguageInfo> languages)
        {
            languages.AddRange(languages);
        }

        public void ChangeDefaultLanguage(string languageName)
        {
            Check.NotNullOrWhiteSpace(languageName, nameof(languageName));

            var language = this.GetEnabledLanguages().FirstOrDefault(o => o.Culture == languageName);
            if (language==null)
            {
                throw new ArgumentException($"Language '{languageName}' was not found!");
            }

            _defaultLanguage = language;
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
            return _defaultLanguage ?? this.GetEnabledLanguages().FirstOrDefault();
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
