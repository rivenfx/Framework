using Microsoft.EntityFrameworkCore;

using System;
using System.Collections.Generic;
using System.Text;

namespace Riven.Identity
{
    public interface IIdentityDbContextAccessor
    {
        DbContext Context { get; }
    }
}
