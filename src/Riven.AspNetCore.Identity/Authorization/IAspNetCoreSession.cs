using Microsoft.Extensions.Options;
using Microsoft.Extensions.DependencyInjection;

using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http;
using Riven.Extensions;

namespace Riven.Authorization
{
    /// <summary>
    /// 本次请求的信息
    /// </summary>
    public interface IAspNetCoreSession
    {
        /// <summary>
        /// 用户Id
        /// </summary>
        string UserId { get; }

        /// <summary>
        /// 用户账号
        /// </summary>
        string UserName { get; }
    }


    public class AspNetCoreSession : IAspNetCoreSession
    {
        protected readonly IdentityOptions _identityOptions;
        protected readonly IHttpContextAccessor _httpContextAccessor;

        public virtual string UserId { get; protected set; }

        public virtual string UserName { get; protected set; }

        public AspNetCoreSession(IServiceProvider serviceProvider)
        {
            this._identityOptions = serviceProvider
                .GetRequiredService<IOptions<IdentityOptions>>().Value;

            this._httpContextAccessor = serviceProvider
               .GetRequiredService<IHttpContextAccessor>();

            var user = this._httpContextAccessor.HttpContext.User;
            this.UserId = user.GetUserId(this._identityOptions);
            this.UserName = user.GetUserName(this._identityOptions);
        }
    }
}
