using System.ComponentModel.DataAnnotations;

namespace BigBangAss2.Models.DTO
{
    public class DoctorRegDTO : Doctor
    {
        public string? Password { get; set; }
        
    }
}
