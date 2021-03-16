using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System;
using Microsoft.AspNetCore.Builder;

namespace Riven.Localization
{
    /// <summary>
    /// Culture 访问器
    /// </summary>
    public interface ICultureAccessor : IDisposable
    {
        /// <summary>
        /// <see cref="RequestLocalizationOptions"/>
        /// </summary>
        RequestLocalizationOptions Options { get; set; }


        /// <summary>
        /// 获取 cookie 或 header 中的 culture 之前。
        /// 返回值不为空则使用返回值做 culture
        /// </summary>
        /// <param name="httpContext"></param>
        /// <returns>culture</returns>
        Task<ProviderCultureResult> OnUserRequestCultureBefore(HttpContext httpContext);

        /// <summary>
        /// 获取 cookie 或 header 中的 culture 之后。
        /// 返回值不为空则使用返回值做 culture
        /// </summary>
        /// <param name="httpContext"></param>
        /// <param name="cookieOrHeaderCulture">cookie或header中的culture标识</param>
        /// <param name="cookieOrHeaderCultureWithUI">cookie或header中的cultureUI标识</param>
        /// <returns></returns>
        Task<ProviderCultureResult> OnUserRequestCultureAfter(HttpContext httpContext, string cookieOrHeaderCulture = null, string cookieOrHeaderCultureWithUI = null);

        /// <summary>
        /// 获取默认本地化 Culture
        /// </summary>
        /// <param name="httpContext"></param>
        /// <returns></returns>
        Task<ProviderCultureResult> GetDefaultRequestCulture(HttpContext httpContext);

        /// <summary>
        /// 获取用户请求本地化 Culture
        /// </summary>
        /// <param name="httpContext"></param>
        /// <returns></returns>
        Task<ProviderCultureResult> GetUserRequestCulture(HttpContext httpContext);

        /// <summary>
        /// 获取请求头本地化 Culture
        /// </summary>
        /// <param name="httpContext"></param>
        /// <returns></returns>
        Task<ProviderCultureResult> GetHeaderRequestCulture(HttpContext httpContext);

        /// <summary>
        /// 获取请求中携带Cookie的 Culture
        /// </summary>
        /// <param name="httpContext"></param>
        /// <returns></returns>
        Task<ProviderCultureResult> GetCookieRequestCulture(HttpContext httpContext);
    }
}
