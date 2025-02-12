using System.ComponentModel.DataAnnotations;

namespace ERPSYS.Models
{
    // public class User
    // {
    //     [Key]
    //     public int Id { get; set; }

    //     [Required, MaxLength(50)]
    //     public string Username { get; set; }

    //     [Required]
    //     public string PasswordHash { get; set; } // Store hashed passwords

    // }
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; }  // This will map to varchar in PostgreSQL
        public string PasswordHash { get; set; } // This will map to varchar in PostgreSQL
    }

}
