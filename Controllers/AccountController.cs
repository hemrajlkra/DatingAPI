using DatingAPI.Data;
using DatingAPI.DTOs;
using DatingAPI.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;

namespace DatingAPI.Controllers
{

    public class AccountController(AppDbContext dbContext) : ApiBaseController
    {
        [HttpPost("register")] //api/account/register
        public async Task<ActionResult<AppUser>> Register(RegisterDto dto)
        {
            using var hmac = new HMACSHA256();
            var user = new AppUser
            {
                Id = Guid.NewGuid().ToString(),
                DisplayName = dto.DisplayName,
                Email = dto.Email,
                PasswordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(dto.Password)),
                PasswordSalt = hmac.Key
            };
            if (await UserExists(dto.Email))
                return BadRequest($"Account already exists with email {dto.Email}");
            dbContext.Users.Add(user);
            dbContext.SaveChanges();
            return Ok(user);
            
        }
        private async Task<bool> UserExists(string email)
        {
            return await dbContext.Users.AnyAsync(x => x.Email.ToLower() == email.ToLower());
        }
        [HttpPost("login")] //api/account/login
        public async Task<ActionResult<AppUser>> Login(LoginDto loginDto)
        {
            var user = await dbContext.Users
                .SingleOrDefaultAsync(x => x.Email.ToLower() == loginDto.Email.ToLower());
            if (user == null)
                return Unauthorized("User not found!!");
            using var hmac = new HMACSHA256(user.PasswordSalt);
            var computeHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(loginDto.Password));
            for(int i = 0; i < computeHash.Length; i++)
            {
                if (computeHash[i] != user.PasswordHash[i])
                    return Unauthorized("invalid email or password!!");
            }
            return Ok(user);
        } 

    }
}
