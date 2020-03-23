using System;
using System.Collections.Generic;
using System.Text;

namespace Riven
{
    /// <summary>
    /// 常量
    /// </summary>
    public static class RivenUnitOfWorkDapperConsts
    {
        /// <summary>
        /// 工作单元选项扩展参数中的 DbContext Provider 名称键值
        /// </summary>
        public const string UnitOfWorkOptionsExtraDataDbContextProviderName = "DbConnectionProviderName";

        /// <summary>
        /// 默认 DbContext 的标识名称
        /// </summary>
        public const string DefaultDbConnectionProviderName = "DefaultDbConnectionProvider";
    }
}
