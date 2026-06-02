using DatingAPI.Entities;

namespace DatingAPI.Interfaces
{
    public interface ITokenService
    {
        public string CreateToken(AppUser user);
        
    }
}
