using Panda.DynamicWebApi;

using System;
using System.Collections.Generic;
using System.Text;

namespace Riven
{
   public static  class DynamicWebApiOptionsExtensions
    {
        public static DynamicWebApiOptions UseRivenDefault(this DynamicWebApiOptions options)
        {
            // 不删除结尾
            options.RemoveActionPostfixes.Clear();
            // 处理ActionName
            options.GetRestFulActionName = (actionName) => actionName;
            // 指定默认的 api 前缀
            options.DefaultApiPrefix = "apis";

            return options;
        }
    }
}
