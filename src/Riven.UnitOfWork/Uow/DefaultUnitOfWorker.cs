using System;
using System.Threading.Tasks;

namespace Riven.Uow
{
    public class DefaultUnitOfWorker : IUnitOfWorker
    {
        protected readonly IServiceProvider _serviceProvider;
        protected readonly IUnitOfWorkManager _uowManager;

        public DefaultUnitOfWorker(IServiceProvider serviceProvider, IUnitOfWorkManager uowManager)
        {
            _serviceProvider = serviceProvider;
            _uowManager = uowManager;
        }

        public T Run<T>(Func<IServiceProvider, IActiveUnitOfWork, T> func)
        {
            Check.NotNull(func, nameof(func));

            if (this._uowManager.Current != null)
            {
                return func.Invoke(this._serviceProvider, this._uowManager.Current);
            }

            using (var uow = this._uowManager.Begin())
            {
                var res = func.Invoke(this._serviceProvider, this._uowManager.Current);
                uow.Complete();

                return res;
            }
        }

        public void Run(Action<IServiceProvider, IActiveUnitOfWork> action)
        {
            Check.NotNull(action, nameof(action));


            this.Run((ioc, currentUow) =>
            {
                action.Invoke(ioc, currentUow);
                return string.Empty;
            });
        }

        public virtual async Task<T> RunAsync<T>(Func<IServiceProvider, IActiveUnitOfWork, Task<T>> func)
        {
            Check.NotNull(func, nameof(func));

            if (this._uowManager.Current != null)
            {
                return await func?.Invoke(this._serviceProvider, this._uowManager.Current);
            }

            using (var uow = this._uowManager.Begin())
            {
                var res = await func?.Invoke(this._serviceProvider, this._uowManager.Current);
                await uow.CompleteAsync();

                return res;
            }
        }

        public virtual async Task RunAsync(Func<IServiceProvider, IActiveUnitOfWork, Task> func)
        {
            Check.NotNull(func, nameof(func));

            await this.RunAsync(async (ioc, currentUow) =>
             {
                 await func?.Invoke(ioc, currentUow);
                 return string.Empty;
             });
        }
    }
}
