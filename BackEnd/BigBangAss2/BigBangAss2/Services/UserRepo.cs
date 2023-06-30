using BigBangAss2.Interfaces;
using BigBangAss2.Models;

namespace BigBangAss2.Services
{
    public class UserRepo : IRepo<User, int>
    {
        public Task<User?> Add(User item)
        {
            throw new NotImplementedException();
        }

        public Task<User?> Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Task<User?> Get(int id)
        {
            throw new NotImplementedException();
        }

        public Task<ICollection<User>?> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<User?> Update(User item)
        {
            throw new NotImplementedException();
        }
    }
}
