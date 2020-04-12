using AspectCore.DynamicProxy;
using Riven.Uow;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Riven.Uow.Extensions;

namespace Riven.Interceptors
{
    public class RivenAspectCoreUnitOfWorkInterceptor : AbstractInterceptor
    {
        public override async Task Invoke(AspectContext context, AspectDelegate next)
        {
            var unitOfWorkManager = context.ServiceProvider.GetService<IUnitOfWorkManager>();
            var unitOfWorkAttr = context.ProxyMethod.GetUnitOfWorkAttributeOrNull();

            if (unitOfWorkAttr == null)
            {
                // Default UnitOfWorkAttribute
                unitOfWorkAttr = context.ServiceProvider.GetService<UnitOfWorkAttribute>();
            }

            if (unitOfWorkAttr.IsDisabled)
            {
                await next(context);
                return;
            }


            var unitOfWorkOptions = unitOfWorkAttr.CreateOptions();

            using (var uow = unitOfWorkManager.Begin(unitOfWorkOptions))
            {
                try
                {
                    await next(context);
                    await uow.CompleteAsync(context.GetHttpContext().RequestAborted);
                }
                catch (Exception e)
                {
                    throw e;
                }
            }
        }
    }
}