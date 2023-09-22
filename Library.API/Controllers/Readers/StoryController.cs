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

        [HttpGet("debt/{readerId}")]
        public async Task<ActionResult<List<BorrowedBook>>> ToReturn(string? readerId)
        {

            if (!int.TryParse(readerId, out int id)) return Unauthorized("Please authorize first");

            var currentReader = await _context.BorrowedBooks
                    .Where(b => b.ReaderID == id && !b.IsReturned && b.ReturneTo <= DateTime.Now)
                    .OrderBy(d => d.ReturneTo)
                    .ToListAsync();

            if (currentReader.Count > 0)
                return currentReader;

            return Ok("No debts");
        }

        [HttpGet("holds/{readerId}")]
        public async Task<ActionResult<List<BorrowedBook>>> Holds(string? readerId)
        {

            if (!int.TryParse(readerId, out int id)) return Unauthorized("Please authorize first");

            var currentReader = await _context.BorrowedBooks
                    .Where(b => b.ReaderID == id && !b.IsReturned)
                    .OrderBy(d => d.ReturneTo)
                    .ToListAsync();
            if(currentReader.Count > 0)
                return currentReader;
            return Ok("No story");
        }

        [HttpGet("story/{readerId}")]
        public async Task<ActionResult<List<BorrowedBook>>> Story(string? readerId)
        {

            if (!int.TryParse(readerId, out int id)) return Unauthorized("Please authorize first");

            var currentReader = await _context.BorrowedBooks
                    .Where(b => b.ReaderID == id)
                    .OrderBy(d => !d.IsReturned)
                    .ThenBy(d => d.ReturneTo)
                    .ToListAsync();

            if (currentReader.Count > 0) 
                return currentReader;

            return Ok("No story");
        }
    }
}
