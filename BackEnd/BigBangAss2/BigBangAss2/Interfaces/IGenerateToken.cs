using BigBangAss2.Models.DTO;

namespace BigBangAss2.Interfaces
{
    public interface IGenerateToken
    {
        public string GenerateToken(UserDTO user);
    }
}
