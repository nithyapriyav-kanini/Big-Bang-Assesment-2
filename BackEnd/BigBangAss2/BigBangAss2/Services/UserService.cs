using BigBangAss2.Interfaces;
using BigBangAss2.Models;
using BigBangAss2.Models.DTO;
using System.Security.Cryptography;
using System.Text;

namespace BigBangAss2.Services
{
    public class UserService : IUserService
    {
        private readonly IRepo<User, int> _URepo;
        private readonly IGenerateToken _tokenGenerate;

        public UserService(IRepo<User,int> URepo,IGenerateToken tokenGenerate)
        {
            _URepo = URepo;
            _tokenGenerate = tokenGenerate;
        }

        public async Task<int> GetUserId(string mail)
        {
            var users = await _URepo.GetAll();
            foreach (var user in users)
            {
                if(user.Email == mail)
                {
                    return user.UserId;
                }
            }
            return -1;
        }

        public async Task<UserDTO?> Login(UserDTO userDTO)
        {
            var id = await GetUserId(userDTO.Email);
            if(id != -1)
            {
                var user = await _URepo.Get(id);
                if (user != null)
                {
                    var hmac = new HMACSHA512(user.HashKey);
                    var userPass = hmac.ComputeHash(Encoding.UTF8.GetBytes(userDTO.Password));
                    for (int i = 0; i < userPass.Length; i++)
                    {
                        if (userPass[i] != user.PasswordHash[i])
                            return null;
                    }
                    userDTO = new UserDTO();
                    userDTO.UserId = user.UserId;
                    userDTO.Email = user.Email;
                    userDTO.Role = user.Role;
                    userDTO.status = user.status;
                    userDTO.Token = _tokenGenerate.GenerateToken(userDTO);
                    return userDTO;
                }
            }
            return null;
        }
    }
}
