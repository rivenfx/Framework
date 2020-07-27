using System;
using System.Collections.Generic;
using System.Text;

namespace Riven.Localization
{
    public class NullCurrentLanguage : ICurrentLanguage
    {
        public LanguageInfo GetCurrentLanguage()
        {
            throw new NotImplementedException();
        }
    }
}
