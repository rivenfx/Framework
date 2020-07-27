using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using System;
using System.Collections.Generic;
using System.Text;

namespace RivenTestsBase
{
    public class TestBaseWithLocalIocManager
    {
        protected IServiceCollection Services { get; private set; }

        protected IServiceProvider ServiceProvider { get; private set; }

        protected IConfiguration Configuration { get; private set; }


        public TestBaseWithLocalIocManager()
        {
            Services = new ServiceCollection();

            Configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: true)
                .Build();
        }


        protected void Build()
        {
            if (ServiceProvider != null)
            {
                return;
            }

            ServiceProvider = Services.BuildServiceProvider();
        }
    }
}
