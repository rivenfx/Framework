using System;
using System.Collections.Generic;
using System.Text;

namespace Riven.Localization
{
    public class NullCurrentLanguage : ICurrentLanguage
    {
        public string Name { get; private set; }

        public NullCurrentLanguage()
        {
            Name = string.Empty;
        }
    }
}
