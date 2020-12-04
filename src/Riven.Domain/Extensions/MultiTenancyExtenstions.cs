using JetBrains.Annotations;

using Microsoft.Extensions.Primitives;

using Riven.MultiTenancy;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Riven.Extensions
{
    public static class MultiTenancyExtenstions
    {
        /// <summary>
        /// 从字典中获取租户名称
        /// </summary>
        /// <param name="keyValuePairs"></param>
        /// <returns></returns>
        public static string GetTenantName([NotNull] this IDictionary<string, StringValues> keyValuePairs, IMultiTenancyOptions multiTenancyOptions)
        {
            Check.NotNull(keyValuePairs, nameof(keyValuePairs));

            var keyValuePair = keyValuePairs
                .FirstOrDefault(o => o.Key.ToLower() == MultiTenancyConfig.TenantNameKey);

            return keyValuePair.Value;
        }
    }
}
