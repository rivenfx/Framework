using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Riven.Identity.Authorization
{
    // services.AddSingleton<IAuthorizationPolicyProvider, ClaimsAuthorizationPolicyProvider>();
    //public class ClaimsAuthorizationPolicyProvider : IAuthorizationPolicyProvider
    //{
    //    public const string POLICY_PREFIX = "Claims";

    //    readonly IServiceProvider _serviceProvider;

    //    public ClaimsAuthorizationPolicyProvider(IServiceProvider serviceProvider)
    //    {
    //        _serviceProvider = serviceProvider;
    //    }

    //    public Task<AuthorizationPolicy> GetDefaultPolicyAsync()
    //    {
    //        return this.GetPolicyAsync(string.Empty);
    //    }

    //    public Task<AuthorizationPolicy> GetFallbackPolicyAsync()
    //    {
    //        return this.GetPolicyAsync(string.Empty);
    //    }

    //    public Task<AuthorizationPolicy> GetPolicyAsync(string policyName)
    //    {
    //        var policy = new AuthorizationPolicyBuilder();
    //        policy.AddRequirements(new ClaimsAuthorizationRequirement(_serviceProvider));
    //        return Task.FromResult(policy.Build());
    //    }
    //}
}
