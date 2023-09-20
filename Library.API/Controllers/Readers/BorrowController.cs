using Librar.DAL;
using Librar.DAL.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Library.API.Controllers.Readers
{
    [Route("api/reader/{readerId}/[controller]")]
    [ApiController]
    public class BorrowController : ControllerBase
    {
        private readonly LibraryContext _context;

        public BorrowController(LibraryContext context)
        {
            this._context = context;
        }

        [HttpPut("take/{bookId}")]
        public async Task<ActionResult> TakeBook(string? readerId, string? bookId)
        {
            if (!int.TryParse(readerId, out var rId) || !int.TryParse(bookId, out var bId))
                return BadRequest();


            var reader = await _context.Readers.FindAsync(rId);
            var book = await _context.Books.FindAsync(bId);

            if (reader == null) return Unauthorized();
            if (book == null || book.Count == 0) return NotFound();


            var returnTo = DateTime.Now.AddDays(book.ReturnDays);
            var bb = new BorrowedBook()
            {
                ReaderID = reader.ID,
                BookID = book.ID,
                Name = book.Name,
                Taked = DateTime.Now,
                ReturneTo = returnTo,
                StoryTook = $"{reader.Name}, take book ( {book.Name} ) for: {returnTo}"
            };
            book.Count--;

            _context.BorrowedBooks.Add(bb);
            _context.SaveChanges();

            return Ok("You borrow book");
        }

        [HttpPut("return/{borowedId}")]
        public async Task<ActionResult> ReturnBook(string? readerId, string? borowedId)
        {

            if (!int.TryParse(readerId, out var rId) || !int.TryParse(borowedId, out var bId))
                return BadRequest();

            var reader = await _context.Readers.FindAsync(rId);
            var toReturn = await _context.BorrowedBooks.FindAsync(bId);
            var toReturnBook = await _context.Books.FindAsync(toReturn?.BookID);

            if (reader == null) return Unauthorized();
            if (toReturn == null || toReturn.IsReturned || toReturnBook == null) return NotFound();

            _context.Books.Attach(toReturnBook);



            toReturn.IsReturned = true;
            toReturn.StoryBack = $"{reader.Name}, return book ( {toReturn.Name} ) || Return Data : {DateTime.Now}";
            if (toReturn.ReturneTo <= DateTime.Now)
                toReturn.WasOverdue = true;

            toReturnBook.Count++;


            _context.SaveChanges();

            return Ok($"{reader.Name}, return book ( {toReturnBook.Name} ) || Return Data : {DateTime.Now}");
        }
    }
}
