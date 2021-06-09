using Microsoft.EntityFrameworkCore;

namespace Riven.Identity
{
    /// <summary>
    /// 简易 Identity DbContext 获取器
    /// </summary>
    /// <typeparam name="TDbContext"></typeparam>
    public class SampleIdentityDbContextAccessor<TDbContext> : IIdentityDbContextAccessor
        where TDbContext : DbContext
    {
        public virtual DbContext Context { get; set; }

        public SampleIdentityDbContextAccessor(TDbContext dbContext)
        {
            this.Context = dbContext;
        }
    }
}
