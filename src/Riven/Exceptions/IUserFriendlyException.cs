namespace Riven.Exceptions
{
    public interface IUserFriendlyException
    {
        /// <summary>
        /// Additional information about the exception.
        /// </summary>
        string Details { get; }

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
