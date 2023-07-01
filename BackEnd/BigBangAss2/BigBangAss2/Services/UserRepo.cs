using BigBangAss2.Interfaces;
using BigBangAss2.Models;
using Microsoft.EntityFrameworkCore;

namespace BigBangAss2.Services
{
    public class UserRepo : IRepo<User, int>
    {
        private HospitalContext _context;

        public UserRepo(HospitalContext context)
        {
            _context=context;
        }
        public async Task<User?> Add(User item)
        {
            try
            {
                _context.Users.Add(item);
                await _context.SaveChangesAsync();
                return item;
            }
            catch (Exception ex)
            {
                throw new Exception();
            }
        }

        public async Task<User?> Delete(int id)
        {
            var user = await Get(id);
            if (user != null)
            {
                try
                {
                    _context.Users.Remove(user);
                    await _context.SaveChangesAsync();
                    return user;
                }
                catch (Exception ex)
                {
                    throw new Exception();
                }
            }
            return null;
        }

        public async Task<User?> Get(int id)
        {
            var result=await  _context.Users.FirstOrDefaultAsync(u=>u.UserId == id);
            if (result != null)
            {
                return result;
            }
            return null;
        }

        public async Task<ICollection<User>?> GetAll()
        {
            var users = await _context.Users.ToListAsync();
            if (users.Count > 0)
            {
                return users;
            }
            return null;
        }

        public async Task<User?> Update(User item)
        {
            var user = await Get(item.UserId);
            if(user != null)
            {
                user.status = item.status;
                await _context.SaveChangesAsync();
                return user;
            }
            return null;
        }
    }
}
