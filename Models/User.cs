using System.ComponentModel.DataAnnotations;

namespace ERPSYS.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        [Required, MaxLength(50)]
        public string Username { get; set; }

        [Required]
        public string PasswordHash { get; set; } // Store hashed passwords
    }

}
