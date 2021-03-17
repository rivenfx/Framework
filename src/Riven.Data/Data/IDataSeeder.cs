using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Riven.Data
{
    /// <summary>
    /// 种子数据处理器
    /// </summary>
    public interface IDataSeeder
    {
        /// <summary>
        /// 运行创建种子数据
        /// </summary>
        /// <param name="context">创建种子数据信息</param>
        /// <returns></returns>
        Task Run(DataSeedContext context);
    }
}
