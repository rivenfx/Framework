using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;

using Panda.DynamicWebApi;
using Panda.DynamicWebApi.Attributes;

using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.DependencyInjection;

namespace Riven.Application
{
    [DynamicWebApi]
    public interface IApplicationService : IDynamicWebApi
    {
    }

    public class ApplicationService : IApplicationService
    {
        public IServiceProvider ServiceProvider { get; set; }

        public virtual T GetService<T>()
        {
           return this.ServiceProvider.GetRequiredService<T>();
        }
    }


}
