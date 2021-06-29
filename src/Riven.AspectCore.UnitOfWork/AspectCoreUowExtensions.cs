using Riven.Uow;
using AspectCore.Configuration;

namespace Riven
{
    /// <summary>
    /// AspectCore工作单元扩展
    /// </summary>
    public static class AspectCoreUowExtensions
    {
        /// <summary>
        /// 添加工作单元拦截器
        /// </summary>
        /// <param name="config"></param>
        /// <returns></returns>
        public static IAspectConfiguration AddRivenUnitOfWork(this IAspectConfiguration config)
        {
            config.Interceptors.AddTyped<UnitOfWrokInterceptor>(method =>
            {
                if (method.IsUnitOfWorkMethod())
                {
                    return true;
                }

                return false;
            });

            return config;
        }
    }
}
