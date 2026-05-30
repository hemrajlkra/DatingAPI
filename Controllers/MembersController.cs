using DatingAPI.Data;
using DatingAPI.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DatingAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MembersController(AppDbContext dbContext) : ControllerBase
    {
        //[HttpGet("{id}")]
        [HttpGet]
        //[EndpointDescription("Get all Members")]
        
        public async Task<ActionResult<IReadOnlyList<AppUser>>> GetMembers()
        {
            var users = await dbContext.Users.ToListAsync();
            return Ok(users);

        }
        [HttpGet("{id}")]
        public async Task<ActionResult<AppUser>> GetMember(string id)
        {
            var user = await dbContext.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }
    }
}
