using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Riven.Uow.Extensions
{
    public static class RivenUnitOfWorkExtensions
    {
        /// <summary>
        /// 从 MethodInfo 获取 UnitOfWorkAttribute
        /// </summary>
        /// <param name="methodInfo"></param>
        /// <returns></returns>
        public static UnitOfWorkAttribute GetUnitOfWorkAttributeOrNull(this MethodInfo methodInfo)
        {
            var attrs = methodInfo.GetCustomAttributes(true)
                            .OfType<UnitOfWorkAttribute>()
                            .ToArray();

            if (attrs.Length > 0)
            {
                return attrs[0];
            }

            attrs = methodInfo.DeclaringType.GetTypeInfo()
                            .GetCustomAttributes(true)
                            .OfType<UnitOfWorkAttribute>()
                            .ToArray();

            if (attrs.Length > 0)
            {
                return attrs[0];
            }


            return null;
        }
    }
}
