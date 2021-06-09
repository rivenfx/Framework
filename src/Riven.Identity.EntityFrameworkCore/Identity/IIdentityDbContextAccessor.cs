using Microsoft.EntityFrameworkCore;

using System;
using System.Collections.Generic;
using System.Text;

namespace Riven.Identity
{
    /// <summary>
    /// Identity DbContext 访问器
    /// </summary>
    public interface IIdentityDbContextAccessor
    {
        /// <summary>
        /// DbContext
        /// </summary>
        DbContext Context { get; }
    }
}
