using System.Threading.Tasks;

using Riven.Dependency;

namespace Riven.Data
{
    /// <summary>
    /// 种子数据执行器
    /// </summary>
    public interface IDataSeedExecutor : ITransientDependency
    {
        /// <summary>
        /// 运行种子数据执行器
        /// </summary>
        /// <param name="dataSeedContext"></param>
        /// <returns></returns>
        Task Run(DataSeedContext dataSeedContext);
    }
}
