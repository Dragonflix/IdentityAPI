using System.ComponentModel.DataAnnotations;

namespace IdentityBLL.Models
{
    public class RoleModel
    {
        [Required]
        public Guid RoleId { get; set; }
        [Required]
        public string RoleName { get; set; }
    }
}
