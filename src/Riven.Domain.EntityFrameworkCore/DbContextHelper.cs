using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Riven
{
    public static class DbContextHelper
    {
        public static string HardDelete { get; } = "HardDelete";

        public static MethodInfo ConfigureGlobalFiltersMethodInfo = typeof(IRivenFilterDbContext).GetMethod(nameof(IRivenFilterDbContext.ConfigureGlobalFilters), BindingFlags.Instance | BindingFlags.Public);


    }
}
