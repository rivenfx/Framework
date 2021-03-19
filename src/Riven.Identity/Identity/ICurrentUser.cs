using Riven.Security;
using Riven.Extensions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Riven.Identity
{
    public interface ICurrentUser
    {
        /// <summary>
        /// 当前信息查找器
        /// </summary>
        ICurrentPrincipalAccessor CurrentPrincipalAccessor { get; }

        /// <summary>
        /// 用户id
        /// </summary>
        string UserId { get; }

        /// <summary>
        /// 用户名
        /// </summary>
        string UserName { get; }
    }

    public class CurrentUser : ICurrentUser
    {
        public virtual ICurrentPrincipalAccessor CurrentPrincipalAccessor => _currentPrincipalAccessor;

        public virtual string UserId { get; protected set; }

        public virtual string UserName { get; protected set; }

        readonly ICurrentPrincipalAccessor _currentPrincipalAccessor;

        public CurrentUser(ICurrentPrincipalAccessor currentPrincipalAccessor)
        {
            _currentPrincipalAccessor = currentPrincipalAccessor;
            UserId = _currentPrincipalAccessor.Principal.GetUserId();
            UserName = _currentPrincipalAccessor.Principal.GetUserName();
        }
    }
}
