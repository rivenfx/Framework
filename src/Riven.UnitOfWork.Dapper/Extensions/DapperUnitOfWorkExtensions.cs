using Riven.Uow;
using System;
using System.Collections.Generic;
using System.Text;

namespace Riven.Extensions
{
    /// <summary>
    /// EFCore工作单元扩展函数
    /// </summary>
    public static class DapperUnitOfWorkExtensions
    {

        /// <summary>
        /// 切换 DbConnectionProviderName
        /// </summary>
        /// <param name="unitOfWork"></param>
        /// <param name="name">provider name</param>
        /// <returns></returns>
        public static IDisposable SetDbConnectionProvider(this IActiveUnitOfWork unitOfWork, string name)
        {
            Check.NotNull(unitOfWork, nameof(unitOfWork));
            Check.NotNullOrWhiteSpace(name, nameof(name));

            return (unitOfWork as DapperUnitOfWork).SetDbConnectionProvider(name);
        }

    }
}
