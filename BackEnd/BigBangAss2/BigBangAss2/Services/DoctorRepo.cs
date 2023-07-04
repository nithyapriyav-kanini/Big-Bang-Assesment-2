using BigBangAss2.Interfaces;
using BigBangAss2.Models;
using Microsoft.EntityFrameworkCore;

namespace BigBangAss2.Services
{
    public class DoctorRepo : IRepo<Doctor, int>
    {
        private HospitalContext _context;

        public DoctorRepo(HospitalContext context)
        {
            _context = context;
        }
        public async Task<Doctor?> Add(Doctor item)
        {
            try
            {
                _context.Doctors.Add(item);
                await _context.SaveChangesAsync();
                return item;
            }
            catch(Exception)
            {
                throw new Exception();
            }
        }

        public async Task<Doctor?> Delete(int id)
        {
            var doctor = await Get(id);
            if(doctor != null)
            {
                try
                {
                    _context.Doctors.Remove(doctor);
                    await _context.SaveChangesAsync();
                    return doctor;
                }
                catch(Exception)
                {
                    throw new Exception();
                }
            }
            return null;
        }

        public async Task<Doctor?> Get(int id)
        {
            var result= await _context.Doctors.FirstOrDefaultAsync(d=> d.DoctorId==id);
            if (result != null)
            {
                return result;
            }
            return null;
        }

        public async Task<ICollection<Doctor>?> GetAll()
        {
            var result =await _context.Doctors.ToListAsync();
            if(result.Count > 0)
            {
                return result;
            }
            return null;
        }

        public async Task<Doctor?> Update(Doctor item)
        {
            var doctor= await Get(item.DoctorId);
            if (doctor != null)
            {
                doctor.Name=item.Name;
                doctor.DOB=item.DOB;
                doctor.Gender=item.Gender;
                doctor.PhoneNo=item.PhoneNo;
                doctor.Specialization=item.Specialization;
                doctor.Experience=item.Experience;
                await _context.SaveChangesAsync();
                return doctor;
            }
            return null;
        }
    }
}
