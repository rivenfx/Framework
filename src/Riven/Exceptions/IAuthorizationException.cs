namespace Riven.Exceptions
{
    public interface IAuthorizationException
    {
        /// <summary>
        /// An arbitrary error code.
        /// </summary>
        int? Code { get; set; }

        /// <summary>
        /// 异常内容
        /// </summary>
        string Message { get; }

    }
}
