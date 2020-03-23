using Riven.Uow;
using System;
using System.Collections.Generic;
using System.Text;

namespace Riven.Extensions
{
    public static class DapperUnitOfWorkOptionsExtensions
    {
        /// <summary>
        /// 获取工作单元选项扩展信息中的 DbConnection Provider Name
        /// </summary>
        /// <param name="unitOfWorkOptions"></param>
        /// <returns></returns>
        public static string GetDbConnectionProviderName(this UnitOfWorkOptions unitOfWorkOptions)
        {
            if (unitOfWorkOptions.ExtraData.TryGetValue(RivenUnitOfWorkDapperConsts.UnitOfWorkOptionsExtraDataDbContextProviderName, out object result))
            {
                return result.ToString();
            }


            return RivenUnitOfWorkDapperConsts.DefaultDbConnectionProviderName;
        }

        /// <summary>
        /// 设置工作单元选项扩展信息中的 DbConnection Provider Name
        /// </summary>
        /// <param name="unitOfWorkOptions"></param>
        /// <param name="name"></param>
        public static void SetDbConnectionProviderName(this UnitOfWorkOptions unitOfWorkOptions, string name)
        {
            Check.NotNullOrWhiteSpace(name, nameof(name));

            unitOfWorkOptions.ExtraData[RivenUnitOfWorkDapperConsts.UnitOfWorkOptionsExtraDataDbContextProviderName] = name;
        }

    }
}
