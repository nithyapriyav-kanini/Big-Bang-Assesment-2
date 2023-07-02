using BigBangAss2.Models;
using BigBangAss2.Models.DTO;

namespace BigBangAss2.Interfaces
{
    public interface IManageService
    {
        public Task<Doctor> RegisterDoctor(DoctorRegDTO dto);
        public Task<Patient> RegisterPatient(PatientRegDTO dto);
        public Task<Doctor> ApproveDoctor(UserIdDTO dto);
        public Task<ICollection<Doctor>> GetDoctors(string state);
        public Task<Boolean> CheckForRepeat(string mail);
        public Task<Doctor> DenyDoctor(UserIdDTO dto);
    }
}
