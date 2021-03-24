using Microsoft.AspNetCore.Identity;

using Riven.Exceptions;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Riven.Extensions
{
    public static class IdentityResultExtensions
    {
        /// <summary>
        /// 检查异常
        /// </summary>
        /// <param name="identityResult"></param>
        public static void CheckError(this IdentityResult identityResult)
        {
            if (identityResult.Succeeded)
            {
                return;
            }

            if (identityResult.Errors == null)
            {
                throw new ArgumentException("identityResult.Errors should not be null.");
            }

            throw new IdentityResultException(identityResult.ToErrorMesssage());
        }


        /// <summary>
        /// 转换成异常消息
        /// </summary>
        /// <param name="identityResult"></param>
        /// <returns></returns>
        public static string ToErrorMesssage(this IdentityResult identityResult)
        {
            Check.NotNull(identityResult, nameof(identityResult));

            if (identityResult.Succeeded)
            {
                return null;
            }

            if (identityResult.Errors == null)
            {
                return "identityResult.Errors has no error messages.";
            }

            return identityResult.Errors.Select(err => err.Description).JoinAsString(", ");
        }


    }
}
