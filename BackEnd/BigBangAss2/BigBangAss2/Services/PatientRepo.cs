using BigBangAss2.Interfaces;
using BigBangAss2.Models;
using Microsoft.EntityFrameworkCore;
using System.Numerics;

namespace BigBangAss2.Services
{
    public class PatientRepo : IRepo<Patient, int>
    {
        private HospitalContext _context;

        public PatientRepo(HospitalContext context)
        {
            _context=context;
        }
        public async Task<Patient?> Add(Patient item)
        {
            try
            {
                _context.Patients.Add(item);
                await _context.SaveChangesAsync();
                return item;
            }
            catch (Exception)
            {
                throw new Exception();
            }
        }

        public async Task<Patient?> Delete(int id)
        {
            var patient = await Get(id);
            if (patient != null)
            {
                try
                {
                    _context.Patients.Remove(patient);
                    await _context.SaveChangesAsync();
                    return patient;
                }
                catch(Exception)
                {
                    throw new Exception();
                }
            }
            return null;
        }

        public async Task<Patient?> Get(int id)
        {
            var result = await _context.Patients.FirstOrDefaultAsync(p => p.PatientId==id);
            if (result != null)
            {
                return result;
            }
            return null;
        }

        public async Task<ICollection<Patient>?> GetAll()
        {
            var patients = await _context.Patients.ToListAsync();
            if (patients.Count > 0)
            {
                return patients;
            }
            return null;
        }

        public async Task<Patient?> Update(Patient item)
        {
            var patient=await Get(item.PatientId);
            if (patient != null)
            {
                patient.Name = item.Name;
                patient.DOB = item.DOB;
                patient.Gender = item.Gender;
                patient.PhoneNo = item.PhoneNo;
                await _context.SaveChangesAsync();
                return patient;
            }
            return null;
        }
    }
}
