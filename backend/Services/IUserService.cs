using System.Linq;
using AeDirectory.DTO;
using AeDirectory.Models;

namespace AeDirectory.Services
{

    public interface IUserService
    {
        bool IsValid(DTO.LoginRequestDTO req);
    }
    public class UserService : IUserService
    {
        private readonly AEV2Context _context;

        public UserService(AEV2Context context)
        {
            _context = context;
        }

        public bool IsValid(LoginRequestDTO req)
        {
            var user = from admin in _context.Admins
                where req.Username.Equals(admin.Username)
                && req.Password.Equals(admin.Password)
                select admin;

            return user.Any();
            
        }
    }
}