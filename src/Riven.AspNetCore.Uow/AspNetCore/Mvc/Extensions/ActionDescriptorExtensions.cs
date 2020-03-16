using Microsoft.AspNetCore.Mvc.Abstractions;
using Riven.Uow;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Riven.AspNetCore.Mvc.Extensions
{
    public static class ActionDescriptorExtensions
    {
        public static UnitOfWorkAttribute GetUnitOfWorkAttributeOrNull(this ActionDescriptor actionDescriptor)
        {
            return actionDescriptor.GetMethodInfo().GetUnitOfWorkAttributeOrNull();
        }


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
