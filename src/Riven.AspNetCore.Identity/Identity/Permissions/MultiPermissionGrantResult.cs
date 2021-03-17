using System.Collections.Generic;

namespace Riven.Identity.Permissions
{
    public class MultiPermissionGrantResult
    {
        public static MultiPermissionGrantResult SuccessResult { get; } = new MultiPermissionGrantResult();


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

        public void SetSuccessed(bool input)
        {
            this.Successed = input;
        }
    }
}
