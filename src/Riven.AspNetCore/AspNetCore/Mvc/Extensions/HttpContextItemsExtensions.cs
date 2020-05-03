using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Riven.AspNetCore.Mvc.Extensions
{
    public static class HttpContextItemsExtensions
    {
        /// <summary>
        /// 权限认证在 HttpContext.Items 中的键值
        /// </summary>
        public const string AUTHORIZATION_ITEM_NAME = "__Riven_Authorization";

        /// <summary>
        /// 
        /// </summary>
        public const string REQUEST_ACTION_INFO_ITEM_NAME = "__Riven_RequestActioinInfo";

        /// <summary>
        /// 添加 权限认证 状态
        /// </summary>
        /// <param name="httpContext"></param>
        /// <param name="input"></param>
        public static void SetAuthorizationException(this HttpContext httpContext, Exception input)
        {
            httpContext?.Items?.Add(AUTHORIZATION_ITEM_NAME, input);
        }

        /// <summary>
        /// 是否存在 权限认证 状态
        /// </summary>
        /// <param name="httpContext"></param>
        /// <returns></returns>
        public static Exception GetAuthorizationException(this HttpContext httpContext)
        {
            if (httpContext.Items.TryGetValue(AUTHORIZATION_ITEM_NAME, out object result))
            {
                return result as Exception;
            }

            return null;
        }

        /// <summary>
        /// 添加请求的 Action 信息
        /// </summary>
        /// <param name="httpContext"></param>
        /// <param name="input"></param>
        public static void SetRequestActionInfo(this HttpContext httpContext, RequestActionInfo input)
        {
            httpContext?.Items?.Add(REQUEST_ACTION_INFO_ITEM_NAME, input);
        }

        /// <summary>
        /// 获取请求的 Action 信息
        /// </summary>
        /// <param name="httpContext"></param>
        /// <returns></returns>
        public static RequestActionInfo GetRequestActionInfo(this HttpContext httpContext)
        {
            if (httpContext.Items.TryGetValue(REQUEST_ACTION_INFO_ITEM_NAME, out object result))
            {
                return result as RequestActionInfo;
            }

            return null;
        }
    }
}
