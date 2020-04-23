using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Riven.Identity.Authorization
{
    public class RoleClaimAuthorizationPolicyProvider : IAuthorizationPolicyProvider
    {
        public const string POLICY_PREFIX = "RoleClaim";

        readonly IServiceProvider _serviceProvider;

        public RoleClaimAuthorizationPolicyProvider(IServiceProvider serviceProvider)
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
            policy.AddRequirements(new RoleClaimAuthorizationRequirement(_serviceProvider));
            return Task.FromResult(policy.Build());
        }
    }
}
