using IdentityDAL.Entities;

namespace IdentityBLL.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class UserModel
    {
        [Required]
        public Guid UserId { get; set; }

        [Required]
        public Guid RoleId { get; set; }

        [Required]
        [StringLength(50)]
        public string RoleName { get; set; }

        [Required]
        [RegularExpression("^(Male|Female)$", ErrorMessage = "Sex must be 'Male' or 'Female'")]
        public string Sex { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [Range(typeof(DateTime), "1900-01-01", "2100-01-01", ErrorMessage = "Birthday must be between 1900 and 2100")]
        public DateTime Birthday { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "First Name must be between 2 and 50 characters.")]
        public string FirstName { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Last Name must be between 2 and 50 characters.")]
        public string LastName { get; set; }

        [Required]
        [EmailAddress(ErrorMessage = "Invalid email format.")]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [StringLength(100, MinimumLength = 8, ErrorMessage = "Password must be at least 8 characters long.")]
        public string Password { get; set; }
    }

}
