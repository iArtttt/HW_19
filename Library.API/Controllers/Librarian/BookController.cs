using Librar.DAL.Models;
using Librar.DAL;
using Microsoft.AspNetCore.Mvc;
using Library.API.Dtos;

namespace Library.API.Controllers.Librarian
{

    [Route("api/librarian/book")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly LibraryContext _context;

        public BookController(LibraryContext context)
        {
            this._context = context;
        }


        [HttpPost("add")]
        public void AddBook(AddBookDto bookDto)
        {

            var curentBook = _context.Books.FirstOrDefault(b => b.Name == bookDto.Name && b.Autor == bookDto.Autor && b.Genre == bookDto.Genre);

            if (curentBook == null)
            {

                Book book = new Book()
                {
                    Name = bookDto.Name,
                    Autor = bookDto.Autor,
                    Genre = bookDto.Genre,
                    Count = bookDto.Count,
                    City = bookDto.City,
                    Country = bookDto.Country,
                    PublishCode = bookDto.PublishCode,
                    PublishCodeType = bookDto.PublishCodeType,
                    ReturnDays = bookDto.ReturnDays,
                    Year = bookDto.Year
                };
                _context.Books.Add(book);
            }
            else
            {
                _context.Books.Attach(curentBook);

                curentBook.Count += bookDto.Count;
            }
            _context.SaveChanges();
        }

        [HttpPost("change/{id = 0}")]
        public void ChangeBook(int? id, ChangeBookDto bookDto)
        {
            if (id == default) return;

            var curentBook = _context.Books.FirstOrDefault(b => b.ID == id);

            if (curentBook != null)
            {
                _context.Books.Attach(curentBook);

                curentBook.Name = bookDto.Name ?? curentBook.Name;
                curentBook.Autor = bookDto.Autor ?? curentBook.Autor;
                curentBook.Genre = bookDto.Genre ?? curentBook.Genre;
                curentBook.Count = bookDto.Count ?? curentBook.Count;
                curentBook.City = bookDto.City ?? curentBook.City;
                curentBook.Country = bookDto.Country ?? curentBook.Country;
                curentBook.PublishCode = bookDto.PublishCode ?? curentBook.PublishCode;
                curentBook.PublishCodeType = bookDto.PublishCodeType ?? curentBook.PublishCodeType;
                curentBook.ReturnDays = bookDto.ReturnDays ?? curentBook.ReturnDays;
                curentBook.Year = bookDto.Year ?? curentBook.Year;

                _context.SaveChanges();
            }
        }

        [HttpPost("remove/{id = 0}")]
        public void RemoveBook(int? id)
        {
            if (id == null) return;

            var curentBook = _context.Books.FirstOrDefault(b => b.ID == id);

            if (curentBook != null)
            {
                _context.Books.Remove(curentBook);
                _context.SaveChanges();
            }

        }

    }
}
