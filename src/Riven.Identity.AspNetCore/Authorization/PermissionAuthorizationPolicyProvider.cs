using Microsoft.AspNetCore.Authorization;

using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Riven.Authorization
{
    public class PermissionAuthorizationPolicyProvider
    {
        readonly IServiceProvider _serviceProvider;

        public PermissionAuthorizationPolicyProvider(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public Task<AuthorizationPolicy> GetDefaultPolicyAsync()
        {
            return this.GetPolicyAsync(string.Empty);
        }

        public Task<AuthorizationPolicy> GetFallbackPolicyAsync()
        {
            return this.GetPolicyAsync(string.Empty);
        }

        public Task<AuthorizationPolicy> GetPolicyAsync(string policyName)
        {
            var policy = new AuthorizationPolicyBuilder();
            policy.AddRequirements(new PermissionAuthorizationRequirement(_serviceProvider));
            return Task.FromResult(policy.Build());
        }
    }
}
