using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BigBangAss2.Models
{
    public class Patient
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int PatientId { get; set; }
        [ForeignKey("PatientId")]
        public User? Users { get; set; }

        [StringLength(50, ErrorMessage = "Name must be between 1 and 50 characters", MinimumLength = 1)]
        public string? Name { get; set; }
        public DateTime DOB { get; set; }

        [StringLength(10, ErrorMessage = "Gender must be between 1 and 10 characters", MinimumLength = 1)]
        public string? Gender { get; set; }
        public int Age
        {
            get
            {
                var year = DateTime.Now.Year - DOB.Year;
                if (DateTime.Now.Month > DOB.Month)
                    year--;
                return year;
            }
            set
            {
                Age = value;
            }
        }

        [StringLength(15, ErrorMessage = "Phone number must be between 1 and 15 characters")]
        public string? PhoneNo { get; set; }
    }
}
