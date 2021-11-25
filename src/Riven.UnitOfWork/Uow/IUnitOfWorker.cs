using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Riven.Uow
{
    /// <summary>
    /// 工作单元工作者
    /// </summary>
    public interface IUnitOfWorker
    {
        /// <summary>
        /// 带返回值的运行 - 异步
        /// </summary>
        /// <typeparam name="T">返回值类型</typeparam>
        /// <param name="func">工作方法</param>
        /// <returns>工作方法的返回值</returns>
        Task<T> RunAsync<T>(Func<IServiceProvider, IActiveUnitOfWork, Task<T>> func);

        /// <summary>
        /// 不带返回值的运行 - 异步
        /// </summary>
        /// <param name="func">工作方法</param>
        /// <returns></returns>
        Task RunAsync(Func<IServiceProvider, IActiveUnitOfWork, Task> func);


        /// <summary>
        /// 带返回值的运行
        /// </summary>
        /// <typeparam name="T">返回值类型</typeparam>
        /// <param name="func">工作方法</param>
        /// <returns>工作方法的返回值</returns>
        T Run<T>(Func<IServiceProvider, IActiveUnitOfWork, T> func);


        /// <summary>
        /// 不带返回值的运行
        /// </summary>
        /// <param name="action">工作方法</param>
        /// <returns></returns>
        void Run(Action<IServiceProvider, IActiveUnitOfWork> action);

    }
}
