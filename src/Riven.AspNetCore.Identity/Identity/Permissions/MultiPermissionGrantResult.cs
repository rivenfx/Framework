using System.Collections.Generic;

namespace Riven.Identity.Permissions
{
    /// <summary>
    /// 多个权限校验结果
    /// </summary>
    public class MultiPermissionGrantResult
    {
        /// <summary>
        /// 默认的成功的
        /// </summary>
        public static MultiPermissionGrantResult SuccessResult { get; } = new MultiPermissionGrantResult(true);


        /// <summary>
        /// 成功
        /// </summary>
        public virtual bool Successed { get; protected set; }

        /// <summary>
        /// 存在错误
        /// </summary>
        public virtual bool HasError { get => Unsuccessful.Count > 0; }

        /// <summary>
        /// 失败的权限
        /// </summary>
        public virtual List<string> Unsuccessful { get; protected set; }



        public MultiPermissionGrantResult()
        {
            Unsuccessful = new List<string>();
        }

        public MultiPermissionGrantResult(bool successed)
            : this()
        {
            Successed = successed;
        }

        public void SetSuccessed(bool input)
        {
            this.Successed = input;
        }
    }
}
