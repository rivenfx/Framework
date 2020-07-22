namespace Riven.Uow
{
    /// <summary>
    /// 默认实现连接字符串名称获取器
    /// </summary>
    public class DefaultCurrentConnectionStringName : ICurrentConnectionStringName
    {
        public virtual string Current => RivenUnitOfWorkConsts.DefaultConnectionStringName;
    }
}
