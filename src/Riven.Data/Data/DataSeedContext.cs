using Riven.Extensions;

using System.Collections.Generic;

namespace Riven.Data
{
    /// <summary>
    /// 种子数据上下文
    /// </summary>
    public class DataSeedContext
    {
        public string TenantName { get; set; }

        public object this[string name]
        {
            get => Properties.GetOrDefault(name);
            set => Properties[name] = value;
        }

        public Dictionary<string, object> Properties { get; }


        public DataSeedContext(string tenantName = null)
        {
            TenantName = tenantName;
            Properties = new Dictionary<string, object>();
        }

        public virtual DataSeedContext WithProperty(string key, object value)
        {
            Properties[key] = value;
            return this;
        }
    }

}
