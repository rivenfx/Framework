using JetBrains.Annotations;

using Microsoft.AspNetCore.Identity;

using System;
using System.Collections.Generic;
using System.Text;

namespace Riven.Identity
{
    public class IdentityInfo
    {
        public static Type PermissionType { get; set; }


        public static IdentityOptions IdentityOptions { get; protected set; }


        public static void Init([NotNull] IdentityOptions identityOptions)
        {
            Check.NotNull(identityOptions, nameof(identityOptions));
            if (IdentityOptions != null)
            {
                throw new Exception($"Repeated initialization {typeof(IdentityInfo).FullName}");
            }

            IdentityOptions = identityOptions;
        }
    }
}
