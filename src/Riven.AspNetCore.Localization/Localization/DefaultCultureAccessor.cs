using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Threading.Tasks;
using JetBrains.Annotations;

using Riven.Extensions;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Builder;

namespace Riven.Localization
{
    public class DefaultCultureAccessor : ICultureAccessor
    {
        private static readonly char[] Separator = { '|' };

        private static readonly string _culturePrefix = "c=";
        private static readonly string _uiCulturePrefix = "uic=";


        protected readonly ILanguageManager _languageManager;
        protected readonly ILogger<DefaultCultureAccessor> _logger;

        public virtual RequestLocalizationOptions Options { get; set; }

        public DefaultCultureAccessor(ILanguageManager languageManager, ILogger<DefaultCultureAccessor> logger)
        {
            _languageManager = languageManager;
            _logger = logger;
        }

        public virtual Task<ProviderCultureResult> OnUserRequestCultureBefore(HttpContext httpContext)
        {
            return Task.FromResult<ProviderCultureResult>(default);
        }

        public virtual Task<ProviderCultureResult> OnUserRequestCultureAfter(HttpContext httpContext, string cookieOrHeaderCulture = null, string cookieOrHeaderCultureWithUI = null)
        {
            if (string.IsNullOrWhiteSpace(cookieOrHeaderCulture)
                && string.IsNullOrWhiteSpace(cookieOrHeaderCultureWithUI))
            {
                var defaultRequestCulture = this.Options?.DefaultRequestCulture;
                if (defaultRequestCulture != null)
                {
                    return Task.FromResult(new ProviderCultureResult(defaultRequestCulture.Culture.Name, defaultRequestCulture.UICulture.Name));
                }
            }


            return Task.FromResult<ProviderCultureResult>(default);
        }

        public virtual async Task<ProviderCultureResult> GetUserRequestCulture(HttpContext httpContext)
        {
            var culture = string.Empty;
            var cultureWithUI = string.Empty;

            #region 从自定义函数 Before 获取

            var beforeUserResult = await this.OnUserRequestCultureBefore(httpContext);
            if (beforeUserResult != null && beforeUserResult.Cultures.Any())
            {
                culture = beforeUserResult.Cultures.First().Value;
                cultureWithUI = beforeUserResult.UICultures.First().Value;

                _logger.LogDebug("{0} {1} - Read from user settings", nameof(GetUserRequestCulture), nameof(OnUserRequestCultureBefore));
                _logger.LogDebug("Using Culture:{0} , UICulture:{1}", culture, cultureWithUI);
                return beforeUserResult;
            }

            #endregion


            ProviderCultureResult result = null;


            #region 从 cookie 中获取

            var cookieResult = await GetCookieRequestCulture(httpContext);
            if (cookieResult != null && cookieResult.Cultures.Any())
            {
                culture = cookieResult.Cultures.First().Value;
                cultureWithUI = cookieResult.UICultures.First().Value;

                _logger.LogDebug("{0} {1} - Read from cookie", nameof(GetUserRequestCulture), nameof(GetCookieRequestCulture));
                _logger.LogDebug("Using Culture:{0} , UICulture:{1}", culture, cultureWithUI);

                result = cookieResult;
            }

            #endregion


            #region 从 Header中获取

            if (result == null || !result.Cultures.Any())
            {
                var headerResult = await this.GetHeaderRequestCulture(httpContext);

                if (headerResult != null && headerResult.Cultures.Any())
                {
                    culture = headerResult.Cultures.First().Value;
                    cultureWithUI = headerResult.UICultures.First().Value;

                    _logger.LogDebug("{0} {1} - Read from header", nameof(GetUserRequestCulture), nameof(GetHeaderRequestCulture));
                    _logger.LogDebug("Using Culture:{0} , UICulture:{1}", culture, cultureWithUI);

                    result = headerResult;
                }
            }

            #endregion



            #region 从可自定义函数 After 中获取

            var afterUserResult = await this.OnUserRequestCultureAfter(httpContext, culture, cultureWithUI);
            if (afterUserResult != null && afterUserResult.Cultures.Any())
            {
                culture = afterUserResult.Cultures.First().Value;
                cultureWithUI = afterUserResult.UICultures.First().Value;

                _logger.LogDebug("{0} {1} - Read from user settings", nameof(GetUserRequestCulture), nameof(OnUserRequestCultureAfter));
                _logger.LogDebug("Using Culture:{0} , UICulture:{1}", culture, cultureWithUI);
                return afterUserResult;
            }

            #endregion

            return result;
        }

        public virtual Task<ProviderCultureResult> GetHeaderRequestCulture(HttpContext httpContext)
        {
            if (httpContext == null)
            {
                throw new ArgumentNullException(nameof(httpContext));
            }

            var localizationHeader = httpContext.Request.Headers[CookieRequestCultureProvider.DefaultCookieName];

            if (localizationHeader.Count == 0)
            {
                return Task.FromResult((ProviderCultureResult)null);
            }

            var cultureResult = ParseHeaderValue(localizationHeader);
            var culture = cultureResult.Cultures.First().Value;
            var uiCulture = cultureResult.UICultures.First().Value;
            _logger.LogDebug("{0} - Using Culture:{1} , UICulture:{2}", nameof(DefaultLocalizationHeaderRequestCultureProvider), culture, uiCulture);

            return Task.FromResult(cultureResult);
        }

        public virtual async Task<ProviderCultureResult> GetCookieRequestCulture(HttpContext httpContext)
        {
            var cookieProvider = httpContext.RequestServices.GetService<CookieRequestCultureProvider>();
            return await GetProviderResultOrNull(httpContext, cookieProvider);
        }

        public virtual async Task<ProviderCultureResult> GetDefaultRequestCulture(HttpContext httpContext)
        {
            var languageManager = httpContext.RequestServices.GetService<ILanguageManager>();
            var defaultLanguage = await languageManager.GetDefaultLanguageAsync();
            var culture = defaultLanguage.Culture;


            if (culture.IsNullOrWhiteSpace())
            {
                return null;
            }

            return new ProviderCultureResult(defaultLanguage.Culture, defaultLanguage.Culture);
        }


        #region 辅助函数


        /// <summary>
        /// Parses a <see cref="RequestCulture"/> from the specified header value.
        /// Returns <c>null</c> if parsing fails.
        /// </summary>
        /// <param name="value">The header value to parse.</param>
        /// <returns>The <see cref="RequestCulture"/> or <c>null</c> if parsing fails.</returns>
        protected virtual ProviderCultureResult ParseHeaderValue(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                return null;
            }

            if (!value.Contains("|") && !value.Contains("="))
            {
                return new ProviderCultureResult(value, value);
            }

            var parts = value.Split(Separator, StringSplitOptions.RemoveEmptyEntries);

            if (parts.Length != 2)
            {
                return null;
            }

            var potentialCultureName = parts[0];
            var potentialUICultureName = parts[1];

            if (!potentialCultureName.StartsWith(_culturePrefix) || !potentialUICultureName.StartsWith(_uiCulturePrefix))
            {
                return null;
            }

            var cultureName = potentialCultureName.Substring(_culturePrefix.Length);
            var uiCultureName = potentialUICultureName.Substring(_uiCulturePrefix.Length);
            var isEmptyCulture = string.IsNullOrWhiteSpace(cultureName);
            var isEmptyUICulture = string.IsNullOrWhiteSpace(uiCultureName);

            if (isEmptyCulture && isEmptyUICulture)
            {
                // No values specified for either so no match
                return null;
            }

            if (!isEmptyCulture && isEmptyUICulture)
            {
                // Value for culture but not for UI culture so default to culture value for both
                uiCultureName = cultureName;
            }

            if (isEmptyCulture && !isEmptyUICulture)
            {
                // Value for UI culture but not for culture so default to UI culture value for both
                cultureName = uiCultureName;
            }

            return new ProviderCultureResult(cultureName, uiCultureName);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="httpContext"></param>
        /// <param name="provider"></param>
        /// <returns></returns>
        protected virtual async Task<ProviderCultureResult> GetProviderResultOrNull([NotNull] HttpContext httpContext, [CanBeNull] IRequestCultureProvider provider)
        {
            if (provider == null)
            {
                return null;
            }

            return await provider.DetermineProviderCultureResult(httpContext);
        }


        #endregion

        public void Dispose()
        {

        }


    }
}
