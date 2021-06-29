using Riven.Uow.Extensions;

using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Riven.Uow
{
    /// <summary>
    /// 工作单元帮助类
    /// </summary>
    public static class UnitOfWorkHelper
    {
        /// <summary>
        /// 是否为工作单元方法
        /// </summary>
        /// <param name="methodInfo">方法信息</param>
        /// <param name="unitOfWorkAttribute">工作单元参数</param>
        /// <returns></returns>
        public static bool IsUnitOfWorkMethod(this MethodInfo methodInfo, out UnitOfWorkAttribute unitOfWorkAttribute)
        {
            unitOfWorkAttribute = methodInfo.GetUnitOfWorkAttributeOrNull();

            if (unitOfWorkAttribute == null || unitOfWorkAttribute.IsDisabled)
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// 是否为工作单元方法
        /// </summary>
        /// <param name="methodInfo">方法信息</param>
        /// <param name="unitOfWorkAttribute">工作单元参数</param>
        /// <returns></returns>
        public static bool IsUnitOfWorkMethod(this MethodInfo methodInfo)
        {
            if (!methodInfo.IsUnitOfWorkMethod(out var uowAttribute) || uowAttribute == null || uowAttribute.IsDisabled)
            {
                return false;
            }

            return true;
        }
    }
}
