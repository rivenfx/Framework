namespace Riven.Entities
{
    /// <summary>
    /// Implement this interface for an entity which may optionally have TenantName.
    /// </summary>
    public interface IMayHaveTenant
    {
        /// <summary>
        /// TenantName of this entity.
        /// </summary>
        string TenantName { get; set; }
    }
}
