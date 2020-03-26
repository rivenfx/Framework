using System;
using System.Collections.Generic;
using System.Text;

namespace Riven.Localization
{
    public interface ILanguageText
    {
        string Value { get; set; }
    }

    public class LanguageText : ILanguageText
    {
        public string Value { get; set; }
    }
}
