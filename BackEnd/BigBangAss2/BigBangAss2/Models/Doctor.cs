using Microsoft.VisualBasic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BigBangAss2.Models
{
    public class Doctor
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int DoctorId { get; set; }
        [ForeignKey("DoctorId")]
        public User? Users { get; set; }

        [StringLength(50, ErrorMessage = "Name must be between 1 and 50 characters", MinimumLength = 1)]
        public string? Name { get; set; }
        public DateTime DOB { get; set; }

        [StringLength(10, ErrorMessage = "Gender must be between 1 and 10 characters", MinimumLength = 1)]
        public string? Gender { get; set; }
        public int Age { get; set; }

        [StringLength(15, ErrorMessage = "Phone number must be between 1 and 15 characters")]
        public string? PhoneNo { get; set; }

        [StringLength(25, ErrorMessage = "Specialization must be between 1 and 25 characters")]
        public string? Specialization { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Experience must be a positive number")]
        public int? Experience { get; set; }
    }
}
