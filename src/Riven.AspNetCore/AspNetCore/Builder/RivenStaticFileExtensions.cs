using Microsoft.AspNetCore.Builder;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Microsoft.AspNetCore.Builder
{
    public static class RivenStaticFileExtensions
    {
        /// <summary>
        ///  启用默认的静态文件服务，并将默认静态文件映射到 baseHref 的 request path
        /// </summary>
        /// <param name="app"></param>
        /// <param name="baseHref">baseHref,必须以“/”开头，若值为 “null”、“/”、空字符 都不会生效</param>
        /// <returns></returns>
        public static IApplicationBuilder UseStaticFilesWithBaseHref(this IApplicationBuilder app,string baseHref)
        {
            // 默认的静态文件
            app.UseStaticFiles();

            // basehref 不等于 / 时需要映射 wwwroot 到 basehref 地址
            if (!string.IsNullOrWhiteSpace(baseHref)
                && baseHref.Trim() != "/")
            {
                app.UseStaticFiles(baseHref.Trim());
            }

            return app;
        }
    }
}
