using Riven.Caching;
using System;
using System.Collections.Generic;
using System.Text;

namespace Riven.Configuration
{
    public class CacheConfigurator : ICacheConfigurator
    {
        public string CacheName { get; private set; }

        public Action<ICacheOptions> InitAction { get; private set; }

        public CacheConfigurator(Action<ICacheOptions> initAction)
        {
            InitAction = initAction;
        }

        public CacheConfigurator(string cacheName, Action<ICacheOptions> initAction)
        {
            CacheName = cacheName;
            InitAction = initAction;
        }
    }
}