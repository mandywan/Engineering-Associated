using AeDirectory.DTO;

namespace AeDirectory.Services
{
    public interface IUserService
    {
        bool IsValid(DTO.LoginRequestDTO req);
    }
    public class UserService : IUserService
    {
        public bool IsValid(LoginRequestDTO req)
        {
            // todo 
            // Avoid hard coding here
            // Need to be verified by DB when admin table is ready
            if (req.Username.Equals("admin") && req.Password.Equals("123456"))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}