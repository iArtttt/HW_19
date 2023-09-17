using Librar.DAL;
using Librar.DAL.Models;
using Library.Common.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
            var currentReader = await _context.BorrowedBooks
                    .Where(b => b.ReaderID == readerId && !b.IsReturned && b.ReturneTo <= DateTime.Now)
                    .OrderBy(d => d.ReturneTo)
                    .ToListAsync();

            if (currentReader != null)
                return currentReader;


            return Unauthorized("Please authorize first");
        }

        [HttpGet("holds/{readerId = 0}")]
        public async Task<ActionResult<List<BorrowedBook>>> Holds(int? readerId)
        {
            var currentReader = await _context.BorrowedBooks
                    .Where(b => b.ReaderID == readerId && !b.IsReturned)
                    .OrderBy(d => d.ReturneTo)
                    .ToListAsync();

            if (currentReader != null) 
                return currentReader;

            return Unauthorized("Please authorize first");
        }

        [HttpGet("story/{readerId = 0}")]
        public async Task<ActionResult<List<BorrowedBook>>> Story(int? readerId)
        {
            var currentReader = await _context.BorrowedBooks
                    .Where(b => b.ReaderID == readerId)
                    .OrderBy(d => !d.IsReturned)
                    .ThenBy(d => d.ReturneTo)
                    .ToListAsync();

            if (currentReader != null) 
                return currentReader;

            return Unauthorized("Please authorize first");
        }
    }
}
