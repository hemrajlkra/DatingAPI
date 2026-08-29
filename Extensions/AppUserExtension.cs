using DatingAPI.DTOs;
using DatingAPI.Entities;
using DatingAPI.Interfaces;
using System.Runtime.CompilerServices;

namespace DatingAPI.Extensions
{
    public static class AppUserExtension
    {
        public static UserDto ToDo(this AppUser user, ITokenService tokenService)
        {
            return new UserDto
            {
                Id = user.Id,
                DisplayName = user.DisplayName,
                Email = user.Email,
                Token = tokenService.CreateToken(user)
            };
        }
    }
}
