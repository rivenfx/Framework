using Microsoft.AspNetCore.Mvc.Filters;
using Riven.AspNetCore.FilterHandlers;
using Riven.AspNetCore.Mvc.Extensions;
using Riven.Uow;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Riven.AspNetCore.Mvc.Uow
{
    public class AspNetCoreUowHandler : IAspNetCoreUnitOfWorkHandler
    {
        protected readonly IServiceProvider _serviceProvider;
        protected readonly IUnitOfWorkManager _unitOfWorkManager;

        public AspNetCoreUowHandler(IServiceProvider serviceProvider, IUnitOfWorkManager unitOfWorkManager)
        {
            _serviceProvider = serviceProvider;
            _unitOfWorkManager = unitOfWorkManager;
        }

        public async Task ActionFilterAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            if (!context.ActionDescriptor.IsControllerAction())
            {
                await next();
                return;
            }

            var unitOfWorkAttr = context.ActionDescriptor.GetUnitOfWorkAttributeOrNull();

            if (unitOfWorkAttr == null)
            {
                // Default UnitOfWorkAttribute
                unitOfWorkAttr = _serviceProvider.GetService<UnitOfWorkAttribute>();
            }

            if (unitOfWorkAttr.IsDisabled)
            {
                await next();
                return;
            }


            var unitOfWorkOptions = unitOfWorkAttr.CreateOptions();

            using (var uow = _unitOfWorkManager.Begin(unitOfWorkOptions))
            {
                var result = await next();
                if (result.Exception == null || result.ExceptionHandled)
                {
                    await uow.CompleteAsync();
                }
            }
        }

        public async Task OnPageHandlerExecutionAsync(PageHandlerExecutingContext context, PageHandlerExecutionDelegate next)
        {
            if (context.HandlerMethod == null)
            {
                await next();
                return;
            }

            var unitOfWorkAttr = context.HandlerMethod.MethodInfo.GetUnitOfWorkAttributeOrNull();

            if (unitOfWorkAttr == null)
            {
                // Default UnitOfWorkAttribute
                unitOfWorkAttr = _serviceProvider.GetService<UnitOfWorkAttribute>();
            }

            if (unitOfWorkAttr.IsDisabled)
            {
                await next();
                return;
            }


            var unitOfWorkOptions = unitOfWorkAttr.CreateOptions();

            using (var uow = _unitOfWorkManager.Begin(unitOfWorkOptions))
            {
                var result = await next();
                if (result.Exception == null || result.ExceptionHandled)
                {
                    await uow.CompleteAsync();
                }
            }
        }
    }
}