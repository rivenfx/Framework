using Microsoft.AspNetCore.Mvc.Filters;
using Riven.AspNetCore.FilterHandlers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Riven.Identity.Authorization
{
    public class RoleClaimAuthorizationHandler : IAspNetCoreAuthorizationHandler
    {
        readonly IServiceProvider _serviceProvider;

        public RoleClaimAuthorizationHandler(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {

        }
    }
}
