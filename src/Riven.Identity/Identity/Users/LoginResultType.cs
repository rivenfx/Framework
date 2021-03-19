using System;
using System.Collections.Generic;
using System.Text;

namespace Riven.Identity.Users
{
    /// <summary>
    /// 登陆结果枚举
    /// </summary>
    public enum LoginResultType
    {
        /// <summary>
        /// 成功
        /// </summary>
        Success = 1,
        /// <summary>
        /// 无效的UserName或Email或PhoneNumber
        /// </summary>
        InvalidUserNameOrEmailAddressOrPhoneNumber = 2,
        /// <summary>
        /// 无效的密码
        /// </summary>
        InvalidPassword = 3,
        /// <summary>
        /// 用户未激活
        /// </summary>
        UserIsNotActive = 4,
        /// <summary>
        /// 用户邮箱未确认
        /// </summary>
        UserEmailIsNotConfirmed = 5,
        /// <summary>
        /// 未知的扩展登陆方法
        /// </summary>
        UnknownExternalLogin = 6,
        /// <summary>
        /// 用已锁定
        /// </summary>
        LockedOut = 7,
        /// <summary>
        /// 用户手机号未激活
        /// </summary>
        UserPhoneNumberIsNotConfirmed = 8,
        /// <summary>
        /// 密码需要重置
        /// </summary>
        PasswordNeedReset = 9,
    }

}
