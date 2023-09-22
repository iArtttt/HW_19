using Librar.DAL.Models;
using Librar.DAL;
using Library.Common.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Library.API.Controllers.Librarian
{
    [Route("api/librarian/info")]
    [ApiController]
    public class InformationController : ControllerBase
    {
        private readonly LibraryContext _context;

        public InformationController(LibraryContext context)
        {
            this._context = context;
        }
        [HttpGet("debtors")]
        public async Task<List<BorrowedBook>> Debtors()
        {

            return await _context.BorrowedBooks
                    .Where(b => !b.IsReturned && b.ReturneTo <= DateTime.Now)
                    .OrderBy(b => b.ReaderID)
                    .ThenBy(d => d.ReturneTo)
                    .ToListAsync();

        }

        [HttpGet("all")]
        public async Task<List<BorrowedBook>> AllDebtors()
        {

            return await _context.BorrowedBooks
                .Where(b => !b.IsReturned)
                .OrderBy(b => b.ReaderID)
                .ThenBy(d => d.ReturneTo)
                .ToListAsync();
        }

        [HttpGet("story/{id = 0}")]
        public async Task<List<BorrowedBook>> Story(int? id)
        {
            return await _context.BorrowedBooks
                .Where(b => b.ReaderID == id)
                .OrderBy(b => !b.IsReturned)
                .ThenBy(d => d.ReturneTo)
                .ToListAsync();
            
        }
    }
}
