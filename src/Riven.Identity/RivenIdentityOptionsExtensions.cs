using Microsoft.AspNetCore.Identity;

using Riven.Identity;

using System;
using System.Collections.Generic;
using System.Text;

namespace Riven
{
    public static class RivenIdentityOptionsExtensions
    {
        /// <summary>
        /// Riven Identity 配置
        /// </summary>
        /// <param name="identityOptions"></param>
        /// <returns></returns>
        public static IdentityOptions ConfigurationRivenIdentity(this IdentityOptions identityOptions)
        {
            IdentityInfo.Init(identityOptions);
            return identityOptions;
        }
    }
}
