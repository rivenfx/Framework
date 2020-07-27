using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Riven.Uow;
using Riven.Uow.Extensions;


namespace Riven.AspNetCore.Mvc.Extensions
{
    public static class ActionDescriptorExtensions
    {
        public static UnitOfWorkAttribute GetUnitOfWorkAttributeOrNull(this ActionDescriptor actionDescriptor)
        {
            return actionDescriptor.GetMethodInfo().GetUnitOfWorkAttributeOrNull();
        }
    }
}
