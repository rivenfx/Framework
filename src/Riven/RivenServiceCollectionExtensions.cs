using Microsoft.Extensions.DependencyInjection;

using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Riven.Security;

namespace Riven
{
    public static class RivenServiceCollectionExtensions
    {

        public static IServiceCollection AddRivenSecurity(this IServiceCollection services)
        {
            services.TryAddSingleton<ICurrentPrincipalAccessor, ThreadCurrentPrincipalAccessor>();
            return services;
        }
    }
}
