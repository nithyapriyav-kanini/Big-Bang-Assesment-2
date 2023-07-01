using BigBangAss2.Exceptions;
using BigBangAss2.Interfaces;
using BigBangAss2.Models;
using BigBangAss2.Models.DTO;
using System.Numerics;
using System.Security.Cryptography;
using System.Text;

namespace BigBangAss2.Services
{
    public class ManageService : IManageService
    {
        private readonly IRepo<Doctor, int> _DRepo;
        private readonly IRepo<User, int> _URepo;
        private readonly IRepo<Patient, int> _PRepo;

        public ManageService(IRepo<Doctor,int> DRepo,IRepo<User,int> URepo,IRepo<Patient,int> PRepo)
        {
            _DRepo = DRepo;
            _URepo = URepo;
            _PRepo = PRepo;
        }

        public async Task<UserDTO> ApproveDoctor(UserDTO dto)
        {
            var user = await _URepo.Get(dto.UserId);
            user.status = dto.status;
            var result = await _URepo.Update(user);
            if (result != null)
            {
                dto.UserId= result.UserId;
                dto.Email= result.Email;
                dto.Password = null;
                dto.status = result.status;
                dto.Role = result.Role;
                return dto;
            }
            return null;
        }

        public async Task<bool> CheckForRepeat(string mail)
        {
            var users = await _URepo.GetAll();
            if (users != null)
            {
                foreach (var user in users)
                {
                    if (user.Email == mail)
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        public async Task<ICollection<Doctor>> GetDoctors()
        {
            var doctors = await _DRepo.GetAll();
            List<Doctor> result= new List<Doctor>();
            foreach(var doctor in doctors)
            {
                if (await CheckApprove(doctor.DoctorId))
                {
                    result.Add(doctor);
                }
            }
            if(result.Count > 0)
            {
                return result;
            }
            return null;
        }
        private async Task<Boolean> CheckApprove(int id)
        {
            var user=await _URepo.Get(id);
            if (user.status == "Approved")
            {
                return true;
            }
            return false;
        } 

        public async Task<Doctor> RegisterDoctor(DoctorRegDTO dto)
        {
            var check = await CheckForRepeat(dto.Users.Email);
            if(check)
            {
                var hmac = new HMACSHA512();
                dto.Users.PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(dto.Password));
                dto.Users.HashKey = hmac.Key;
                dto.Users.Role = "Doctor";
                dto.Users.status = "Requested";
                var result = await _URepo.Add(dto.Users);
                if (result != null)
                {
                    dto.DoctorId = result.UserId;
                    var doctor = await Mapper(dto);
                    var res = await _DRepo.Add(doctor);
                    if(res != null)
                    {
                        return res;
                    }
                }
            }
            else
            {
                throw new RepeatationException();
            }
            return null;
        }

        public async Task<Patient> RegisterPatient(PatientRegDTO dto)
        {
            var check = await CheckForRepeat(dto.Users.Email);
            if (check)
            {
                var hmac = new HMACSHA512();
                dto.Users.PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(dto.Password));
                dto.Users.HashKey = hmac.Key;
                dto.Users.Role = "Patient";
                var result = await _URepo.Add(dto.Users);
                if (result != null)
                {
                    dto.PatientId= result.UserId;
                    var patient = await Mapper(dto);
                    var res = await _PRepo.Add(patient);
                    if (res != null)
                    {
                        return res;
                    }
                }
            }
            else
            {
                throw new RepeatationException();
            }
            return null;
        }


        private async Task<Doctor> Mapper(DoctorRegDTO dto)
        {
            Doctor doctor= new Doctor();
            doctor.DoctorId = dto.DoctorId;
            doctor.Name = dto.Name;
            doctor.DOB = dto.DOB;
            var year = DateTime.Now.Year - dto.DOB.Year;
            if (DateTime.Now.Month > dto.DOB.Month)
                year--;
            doctor.Age = year;
            doctor.Gender = dto.Gender;
            doctor.PhoneNo = dto.PhoneNo;
            doctor.Specialization = dto.Specialization;
            doctor.Experience = dto.Experience;
            return doctor;
        }
        private async Task<Patient> Mapper(PatientRegDTO dto)
        {
            Patient patient = new Patient();
            patient.PatientId= dto.PatientId;
            patient.Name = dto.Name;
            patient.DOB = dto.DOB;
            var year = DateTime.Now.Year - dto.DOB.Year;
            if (DateTime.Now.Month > dto.DOB.Month)
                year--;
            patient.Age = year;
            patient.Gender = dto.Gender;
            patient.PhoneNo = dto.PhoneNo;
            return patient;
        }

    }
}
