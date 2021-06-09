using System.Collections.Generic;
using System.Text;

namespace Riven.Identity.Permissions
{
    /// <summary>
    /// 权限初始化器
    /// </summary>
    public interface IPermissionInitializer
    {
        /// <summary>
        /// 运行
        /// </summary>
        IEnumerable<PermissionItem> Run();

    }
}
