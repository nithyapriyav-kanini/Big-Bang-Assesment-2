using BigBangAss2.Interfaces;
using BigBangAss2.Models;
using BigBangAss2.Services;
using Microsoft.EntityFrameworkCore;

namespace BigBangAss2Test
{
    [TestClass]
    public class UnitTest1
    {
        public DbContextOptions<HospitalContext> GetDbcontextOption()
        {
            var contextOptions = new DbContextOptionsBuilder<HospitalContext>()
                                   .UseInMemoryDatabase(databaseName: "hospitalMemory")
                                    .Options;
            return contextOptions;
        }

        [TestMethod]
        public async Task TestGetAllPatientRepo()
        {
            using (var hospitalContext = new HospitalContext(GetDbcontextOption()))
            {

                hospitalContext.Patients.Add(new Patient
                {
                    PatientId = 1,
                    Name = "Somu s",
                    DOB = new DateTime(2001, 09, 12),
                    Age = 22,
                    PhoneNo = "1234567890",
                    Gender = "Male",
                    Users = new User() { UserId = 1,Email="somu@gmail.com", PasswordHash = new byte[] { },HashKey = new byte[] { }, Role = "Patient", status = "Not Approved" },
                }); ; ;
                await hospitalContext.SaveChangesAsync();
                IRepo<User, int> repo = new UserRepo(hospitalContext);
                var data = await repo.GetAll();
                Assert.AreEqual(1, data.ToList().Count);

            }

        }


        [TestMethod]
        public async Task TestGetAllDoctorRepo()
        {
            using (var hospitalContext = new HospitalContext(GetDbcontextOption()))
            {

                hospitalContext.Doctors.Add(new Doctor
                {
                    DoctorId = 1,
                    Name = "Somu s",
                    DOB = new DateTime(2001, 09, 12),
                    Age = 22,
                    PhoneNo = "1234567890",
                    Gender = "Male",
                    Specialization = "Neuro",
                    Experience = 2,
                    Users = new User() { UserId = 1, Email = "somu@gmail.com", PasswordHash = new byte[] { }, HashKey = new byte[] { }, Role = "Patient", status = "Not Approved" },
                }); ; ;
                await hospitalContext.SaveChangesAsync();
                IRepo<User, int> repo = new UserRepo(hospitalContext);
                var data = await repo.GetAll();
                Assert.AreEqual(1, data.ToList().Count);

            }

        }

    }
}