using System.ComponentModel.DataAnnotations;

namespace BigBangAss2.Models
{
    public class User
    {
        [Key]
        public int UserId { get; set; }

        [StringLength(50, ErrorMessage = "Email must be between 1 and 50 characters")]
        [EmailAddress(ErrorMessage = "Invalid email address")]
        public string? Email { get; set; }
        public byte[]? PasswordHash { get; set; }
        public byte[]? HashKey { get; set; }
        public string? Role { get; set; }
        public string? status { get; set; }
    }
}
