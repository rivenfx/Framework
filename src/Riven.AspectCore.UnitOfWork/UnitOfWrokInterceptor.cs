using AspectCore.DynamicProxy;

using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Riven.Uow;

namespace Riven
{
    /// <summary>
    /// 工作单元拦截器
    /// </summary>
    public class UnitOfWrokInterceptor : AbstractInterceptor
    {
        public override async Task Invoke(AspectContext context, AspectDelegate next)
        {

            // 实现方法是否为uow
            var implementationMethodIsUow = UnitOfWorkHelper.IsUnitOfWorkMethod(context.ImplementationMethod, out var uowAttribute);

            // 实现方法没有标识uow
            if (!implementationMethodIsUow)
            {
                // 工作单元已禁用
                if (uowAttribute != null && uowAttribute.IsDisabled)
                {
                    await next(context);
                    return;
                }


                // 获取代理的方法是否为uow
                if (!UnitOfWorkHelper.IsUnitOfWorkMethod(context.ProxyMethod, out uowAttribute))
                {
                    // 如果不是uow，跳过
                    await next(context);
                    return;
                }
            }

            // 当前连接字符串名称
            var currentConnectionStringName = context.ServiceProvider.GetService<ICurrentConnectionStringNameProvider>()?.Current;
            // 创建选项
            var unitOfWorkOptions = uowAttribute.CreateOptions(currentConnectionStringName);
            // 工作单元管理器
            var unitOfWorkManager = context.ServiceProvider.GetRequiredService<IUnitOfWorkManager>();
            // 启用工作单元
            using (var uow = unitOfWorkManager.Begin(unitOfWorkOptions))
            {
                await next(context);
                await uow.CompleteAsync();
            }
        }
    }
}
