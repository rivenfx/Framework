using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using JetBrains.Annotations;

namespace Riven.Localization
{
    public class DefaultLanguageManager : ILanguageManager
    {
        protected static Dictionary<string, LanguageInfo> Data;

        public DefaultLanguageManager()
        {
            if (Data == null)
            {
                Data = new Dictionary<string, LanguageInfo>();
            }

        }

        public void Add([NotNull]LanguageInfo language)
        {
            Data[language.Name] = language;
        }

        public void AddRange([NotNull]List<LanguageInfo> languages)
        {
            languages.AddRange(languages);
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
