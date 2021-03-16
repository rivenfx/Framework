namespace Riven.Uow
{
    /// <summary>
    /// 默认实现连接字符串名称提供者
    /// </summary>
    public class DefaultCurrentConnectionStringNameProvider : ICurrentConnectionStringNameProvider
    {
        public virtual string Current { get; }
    }
}
