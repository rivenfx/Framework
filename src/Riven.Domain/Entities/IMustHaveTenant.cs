using System.ComponentModel.DataAnnotations;

namespace Riven.Entities
{
    /// <summary>
    /// Implement this interface for an entity which must have TenantName.
    /// </summary>
    public interface IMustHaveTenant
    {
        /// <summary>
        /// TenantName of this entity.
        /// </summary>
        [Required]
        string TenantName { get; set; }
    }
}
