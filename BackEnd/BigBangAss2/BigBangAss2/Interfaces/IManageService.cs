using BigBangAss2.Models;
using BigBangAss2.Models.DTO;

namespace BigBangAss2.Interfaces
{
    public interface IManageService
    {
        public Task<Doctor> RegisterDoctor(DoctorRegDTO dto);
        public Task<Patient> RegisterPatient(PatientRegDTO dto);
        public Task<UserDTO> ApproveDoctor(UserDTO dto);
        public Task<ICollection<Doctor>> GetDoctors();
        public Task<Boolean> CheckForRepeat(string mail);
    }
}
