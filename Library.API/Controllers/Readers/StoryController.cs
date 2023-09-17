using Librar.DAL;
using Librar.DAL.Models;
using Library.Common.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Library.API.Controllers.Readers
{
    [Route("api/reader/inform")]
    [ApiController]
    public class StoryController : ControllerBase
    {
        private readonly LibraryContext _context;

        public StoryController(LibraryContext context) 
        {
            this._context = context;
        }

        [HttpGet("debt/{readerId = 0}")]
        public async Task<ActionResult<List<BorrowedBook>>> ToReturn(int? readerId)
        {
            var currentReader = await _context.Readers.FindAsync(readerId);

            if (currentReader != null)
            {
                return currentReader.BorrowedBooks
                    .Where(b => !b.IsReturned && b.ReturneTo <= DateTime.Now)
                    .OrderBy(d => d.ReturneTo)
                    .ToList();

            }

            return Unauthorized("Please authorize first");
        }

        [HttpGet("holds/{readerId = 0}")]
        public async Task<ActionResult<List<BorrowedBook>>> Holds(int? readerId)
        {
            var currentReader = await _context.Readers.FindAsync(readerId);

            if (currentReader != null)
            {
                return currentReader.BorrowedBooks
                    .Where(b => !b.IsReturned)
                    .OrderBy(d => d.ReturneTo)
                    .ToList();

            }

            return Unauthorized("Please authorize first");
        }

        [HttpGet("story/{readerId = 0}")]
        public async Task<List<BorrowedBook>> Story(int? readerId)
        {
            var currentReader = await _context.Readers.FindAsync(readerId);

            if (currentReader != null)
            {
                return currentReader.BorrowedBooks.Select(b => b)
                    .OrderBy(d => d.ReturneTo)
                    .ToList();

            }

            return null/*("Please authorize first")*/;
        }
    }
}
