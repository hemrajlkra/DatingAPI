using System.ComponentModel.DataAnnotations;

namespace DatingAPI.DTOs
{
    public class LoginDto
    {
        [EmailAddress]
        public required string Email { get; set; }
        public required string Password { get; set; }

    }
}
