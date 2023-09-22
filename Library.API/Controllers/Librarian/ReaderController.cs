using Librar.DAL;
using Librar.DAL.Models;
using Library.API.Dtos;
using Library.Common.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Library.API.Controllers.Librarian
{
    [Route("api/librarian/[controller]")]
    [ApiController]
    public class ReaderController : ControllerBase
    {
        private readonly LibraryContext _context;

        public ReaderController(LibraryContext context) 
        {
            this._context = context;
        }

        [HttpPost("registration")]
        public async Task<ActionResult<IUser>> RegistrationReader(UserRegistrationDto regInfo)
        {
            var users = await _context.Librarians.ToListAsync<IUser>();
            users.AddRange(await _context.Readers.ToListAsync<IUser>());

            if(!users.Any(u => u.Login == regInfo.Login))
            {
                var reader = new Reader()
                {
                    Login = regInfo.Login,
                    Password = regInfo.Password,
                    Mail = regInfo.Email,
                    Name = regInfo.Name,
                    LastName = regInfo.LastName,
                    SecondName = regInfo.SecondName,
                    DocumentNumber = regInfo.DocumentNumber,
                    DocumentType = regInfo.DocumentType,
                    Birthday = regInfo.Birthday
                };

                _context.Readers.Add(reader);
                _context.SaveChanges();
                return Ok("Register succeed");
            }

            return BadRequest("User with this login has already exist");

        }

        [HttpPut("Change/{id = 0}")]
        public async Task<ActionResult<IUser>> ChangeReader(int? id, UserChangingDto regInfo)
        {
            var reader = await _context.Readers.FindAsync(id);

            if(reader != null)
            {
                reader.Login = regInfo.Login??reader.Login;
                reader.Password = regInfo.Password??reader.Password;
                reader.Mail = regInfo.Email??reader.Mail;
                reader.Name = regInfo.Name??reader.Name;
                reader.LastName = regInfo.LastName??reader.LastName;
                reader.SecondName = regInfo.SecondName??reader.SecondName;
                reader.DocumentNumber = regInfo.DocumentNumber??reader.DocumentNumber;
                reader.DocumentType = regInfo.DocumentType??reader.DocumentType;
                reader.Birthday = regInfo.Birthday?? reader.Birthday;

                _context.SaveChanges();
                return Ok("Change succeed");
            }

            return BadRequest("This reader doesn't found");

        }

        [HttpDelete("remove/{id = 0}")]
        public async Task<ActionResult<IUser>> RemoveReader(int? id)
        {
            var readerToRemove = await _context.Readers.FindAsync(id);
            if (readerToRemove != default)    
            {
                
                _context.Readers.Remove(readerToRemove);
                _context.SaveChanges();
                return Ok("Remove succeed");
            }

            return BadRequest("User with this ID does't exist");

        }
    }
}
