using JetBrains.Annotations;

using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Riven.Extensions
{
    public static class HttpContextRequestExtensions
    {
        public const string RequestedWithHeader = "X-Requested-With";
        public const string RequestedWithHeaderLower = "x-requested-with";
        public const string XmlHttpRequest = "XMLHttpRequest";

        public const string AcceptHeader = "Accept";

        public const string RefererHeader = "Referer";
        public const string RefererFromSwagger = "swagger";

        public static bool IsAjax([NotNull] this HttpContext httpContext)
        {
            Check.NotNull(httpContext, nameof(httpContext));
            Check.NotNull(httpContext.Request, nameof(httpContext.Request));

            if (httpContext.Request.Headers == null)
            {
                return false;
            }

            if (httpContext.Request.Headers.TryGetValue(RefererHeader, out StringValues refererValues)
                && refererValues.ToString().Contains(RefererFromSwagger))
            {
                return true;
            }

            var ajaxHeader = httpContext.Request.Headers
                .FirstOrDefault(o => o.Key == RequestedWithHeaderLower || o.Key == RequestedWithHeader);

            return ajaxHeader.Value == XmlHttpRequest;
        }

        public static bool CanAccept([NotNull] this HttpContext httpContext, [NotNull] string contentType)
        {
            Check.NotNull(httpContext, nameof(httpContext));
            Check.NotNull(httpContext.Request, nameof(httpContext.Request));
            Check.NotNull(contentType, nameof(contentType));

            return httpContext.Request.Headers[AcceptHeader].ToString().Contains(contentType);
        }
    }
}
