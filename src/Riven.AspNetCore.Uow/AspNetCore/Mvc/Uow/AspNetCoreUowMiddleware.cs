using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Riven.Uow;
using System.Net;
using Microsoft.Extensions.Options;

namespace Riven.AspNetCore.Mvc.Uow
{
    public class AspNetCoreUowMiddleware : IMiddleware
    {
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            if (context.Request.RouteValues.Count == 0)
            {
                await next(context);
                return;
            }

            var serviceProvider = context.RequestServices;

            var endpoint = context.GetEndpoint();
            var unitOfWorkAttribute = endpoint?.Metadata?.GetMetadata<UnitOfWorkAttribute>();

            // 为空的话获取默认的工作单元配置
            if (unitOfWorkAttribute == null)
            {
                unitOfWorkAttribute = serviceProvider.GetRequiredService<IOptions<UnitOfWorkAttribute>>().Value;
            }

            // 如果是禁用
            if (unitOfWorkAttribute.IsDisabled)
            {
                await next(context);
                return;
            }

            // 当前连接字符串名称
            var currentConnectionStringName = serviceProvider.GetService<ICurrentConnectionStringNameProvider>()?.Current;

            // 创建选项
            var unitOfWorkOptions = unitOfWorkAttribute.CreateOptions(currentConnectionStringName);

            // 启动工作单元
            var unitOfWorkManager = serviceProvider.GetRequiredService<IUnitOfWorkManager>();
            using (var uow = unitOfWorkManager.Begin(unitOfWorkOptions))
            {
                await next(context);

                // 只有响应码为200的时候才提交更改
                if (context.Response.StatusCode == (int)HttpStatusCode.OK)
                {
                    await uow.CompleteAsync(context.RequestAborted);
                }
            }
        }
    }
}
