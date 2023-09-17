using Librar.DAL.Models;
using Librar.DAL;
using Library.API.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Library.API.Controllers.Librarian
{

    [Route("api/librarian/[controller]")]
    [ApiController]
    public class AutorController : ControllerBase
    {
        private readonly LibraryContext _context;

        public AutorController(LibraryContext context)
        {
            this._context = context;
        }

        [HttpPost("add")]
        public void AddAutor(AddAutorDto autorDto)
        {

            Autor autor = new Autor()
            {
                Name = autorDto.Name,
                LastName = autorDto.LastName,
                SecondName = autorDto.SecondName,
                Birthday = autorDto.Birthday,
            };
            _context.Autors.Add(autor);
            _context.SaveChanges();
        }

        [HttpPost("change/{id = 0}")]
        public void ChangeAutor(int? id, ChangeAutorDto autorDto)
        {
            if (id == default) return;

            var curentBook = _context.Autors.FirstOrDefault(b => b.ID == id);

            if (curentBook != null)
            {
                _context.Autors.Attach(curentBook);

                curentBook.Name = autorDto.Name ?? curentBook.Name;
                curentBook.LastName = autorDto.LastName ?? curentBook.LastName;
                curentBook.SecondName = autorDto.SecondName ?? curentBook.SecondName;
                curentBook.Birthday = autorDto.Birthday ?? curentBook.Birthday;

                _context.SaveChanges();
            }
        }

        [HttpPost("remove/{id = 0}")]
        public void RemoveAutor(int? id)
        {
            if (id == null) return;

            var curentBook = _context.Autors.FirstOrDefault(b => b.ID == id);

            if (curentBook != null)
            {
                _context.Autors.Remove(curentBook);
                _context.SaveChanges();
            }

        }

    }
}
