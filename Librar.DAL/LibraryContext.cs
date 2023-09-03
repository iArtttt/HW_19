using Librar.DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace Librar.DAL
{
    public class LibraryContext : DbContext
    {

        public DbSet<Librarian> Librarians { get; set; }
        public DbSet<Reader> Readers { get; set; }
        public DbSet<Autor> Autors { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<DocumentType> DocumentTypes { get; set; }
        public DbSet<PublishingCodeType> PublishingCodeTypes { get; set; }
        public DbSet<BooksAutorsRelation> BooksAutorsRelations { get; set; }

        public LibraryContext(DbContextOptions optionsBuilder)
            : base(optionsBuilder)
        {
        }
    }
}
