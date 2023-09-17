using Librar.DAL;
using Librar.DAL.Models;
using Library.API.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Library.API.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class SearchController : ControllerBase
    {
        private readonly LibraryContext _context;

        public SearchController(LibraryContext context)
        {
            this._context = context;
        }
        [HttpPost("book")]
        public async Task<List<Book>> SearchByBook(SearchDto search)
        {
            
            if (!string.IsNullOrEmpty(search.Search))
                return await _context.Books.Where(n => n.Name.Contains(search.Search)).ToListAsync();
            else
                return await _context.Books.ToListAsync();

        }
        [HttpPost("autor")]
        public async Task<List<Book>> SearchByAutor(SearchDto search)
        {
            if (!string.IsNullOrEmpty(search.Search))
                return await _context.Autors.Where(n => n.Name.Contains(search.Search)).SelectMany(b => b.Books.Select(s => s)).ToListAsync();
            else
                return await _context.Autors.SelectMany(b => b.Books.Select(s => s)).ToListAsync();

        }

    }
}
