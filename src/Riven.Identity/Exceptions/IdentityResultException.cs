using JetBrains.Annotations;

using Microsoft.AspNetCore.Identity;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Riven.Extensions;

namespace Riven.Exceptions
{
    public class IdentityResultException : Exception
    {
        public IdentityResultException([NotNull] IdentityResult identityResult)
           : base(identityResult.ToErrorMesssage())
        {
            Check.NotNull(identityResult, nameof(identityResult));
        }

        public IdentityResultException([NotNull] string identityResultErrorMessage)
         : base(identityResultErrorMessage)
        {
            Check.NotNullOrWhiteSpace(identityResultErrorMessage, nameof(identityResultErrorMessage));
        }
    }
}
