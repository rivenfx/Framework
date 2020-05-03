using JetBrains.Annotations;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Riven.Extensions
{
    public static class HttpContextRequestExtensions
    {
        public const string RequestedWithHeader = "X-Requested-With";
        public const string XmlHttpRequest = "XMLHttpRequest";

        public static bool IsAjax([NotNull]this HttpContext httpContext)
        {
            Check.NotNull(httpContext, nameof(httpContext));
            Check.NotNull(httpContext.Request, nameof(httpContext.Request));

            if (httpContext.Request.Headers == null)
            {
                return false;
            }

            return httpContext.Request.Headers[RequestedWithHeader] == XmlHttpRequest;
        }

        public static bool CanAccept([NotNull]this HttpContext httpContext, [NotNull] string contentType)
        {
            Check.NotNull(httpContext, nameof(httpContext));
            Check.NotNull(httpContext.Request, nameof(httpContext.Request));
            Check.NotNull(contentType, nameof(contentType));

            return httpContext.Request.Headers["Accept"].ToString().Contains(contentType);
        }
    }
}
