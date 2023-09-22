using Librar.DAL;
using Library.API.Dtos;
using Library.Common.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Library.API.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly LibraryContext _context;

        public LoginController(LibraryContext context) 
        {
            this._context = context;
        }

        [HttpPost("enter")]
        public async Task<ActionResult<IUser>> Enter([FromBody]UserLoginDto userLoginDto)
        {
            var users = await _context.Librarians.ToListAsync<IUser>();
            users.AddRange(await _context.Readers.ToListAsync<IUser>());

            var currentUser = users.FirstOrDefault(l => l.Login == userLoginDto.Login);

            if (currentUser == null)
                return BadRequest("User with this login doesn`t exist");

            if (currentUser.Password != userLoginDto.Password)
                return Unauthorized("Incorrect password")/* BadRequest("Incorrect password")*/;

            return Ok("You are in system");
        }
    }
}
