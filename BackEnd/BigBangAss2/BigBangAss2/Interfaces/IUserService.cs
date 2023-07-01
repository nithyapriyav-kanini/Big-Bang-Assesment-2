using BigBangAss2.Models.DTO;

namespace BigBangAss2.Interfaces
{
    public interface IUserService
    {
        public Task<UserDTO?> Login(UserDTO userDTO);
        public Task<int> GetUserId(string mail);
    }
}
