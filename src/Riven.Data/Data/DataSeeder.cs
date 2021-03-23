using System;
using System.Threading.Tasks;

using Microsoft.Extensions.DependencyInjection;

namespace Riven.Data
{
    public class DataSeeder : IDataSeeder
    {
        protected readonly IServiceProvider _serviceProvider;

        public DataSeeder(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public async Task Run(DataSeedContext context)
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var executors = scope.ServiceProvider.GetServices<IDataSeedExecutor>();
                foreach (var item in executors)
                {
                    await item.Run(context);
                }
            }
        }
    }
}
