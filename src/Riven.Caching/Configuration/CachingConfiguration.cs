using Riven.Caching;
using System;
using System.Collections.Generic;

namespace Riven.Configuration
{
    public class CachingConfiguration : ICachingConfiguration
    {
        public IReadOnlyList<ICacheConfigurator> Configurators
        {
            get { return _configurators.ToList(); }
        }
        private readonly List<ICacheConfigurator> _configurators;

        public CachingConfiguration()
        {
            _configurators = new List<ICacheConfigurator>();
        }

        public void ConfigureAll(Action<ICacheOptions> initAction)
        {
            _configurators.Add(new CacheConfigurator(initAction));
        }

        public void Configure(string cacheName, Action<ICacheOptions> initAction)
        {
            _configurators.Add(new CacheConfigurator(cacheName, initAction));
        }
    }
}

