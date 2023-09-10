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
        public DbSet<BookGenre> BookGenres { get; set; }
        public DbSet<BorrowedBook> BorrowedBooks { get; set; }

        public LibraryContext(DbContextOptions optionsBuilder)
            : base(optionsBuilder)
        {
            Database.EnsureDeleted();
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Librarian>()
                .HasMany(l => l.Readers)
                .WithOne(r => r.Librarian)
                .HasForeignKey(r => r.LibrarianLog)
                .HasPrincipalKey(pk => pk.Login);

            modelBuilder.Entity<Book>()
                .HasOne(an => an.AutorNavigation)
                .WithMany(b => b.Books)
                .HasForeignKey(a => a.Autor)
                .HasPrincipalKey(f => f.Name);

            modelBuilder.Entity<Book>()
                .HasOne(a => a.GenreId)
                .WithMany(b => b.Books)
                .HasForeignKey(g => g.Genre)
                .HasPrincipalKey(g => g.Genre);

            modelBuilder.Entity<Book>()
                .HasOne(a => a.PublishingCodeTypeNavigation)
                .WithMany(b => b.Books)
                .HasForeignKey(g => g.PublishCodeType)
                .HasPrincipalKey(c => c.CodeType);

            modelBuilder.Entity<Reader>()
                .HasOne(a => a.DocumentTypeNavigation)
                .WithMany(b => b.Readers)
                .HasForeignKey(d => d.DocumentType)
                .HasPrincipalKey(d => d.Type);


            modelBuilder.Entity<BorrowedBook>()
                .HasOne(a => a.Reader)
                .WithMany(b => b.BorrowedBooks)
                .HasForeignKey(s => s.ReaderID)
                .HasForeignKey(s => s.ReaderID);



            modelBuilder.Entity<Librarian>().HasData(
                new Librarian { ID = 1, Login = "Admin", Password = "1234", Mail = "admin@gmail.com" }
                );
            modelBuilder.Entity<Reader>().HasData(
                new Reader { 
                    ID = 1,
                    Login = "Reader", 
                    Password = "1234", 
                    Mail = "reader@gmail.com", 
                    Name = "Bob", 
                    Birthday = DateTime.Today , 
                    DocumentType = "Passport",
                    DocumentNumber = 3354213,
                    LibrarianLog = "Admin"
                },
                new Reader {
                    ID = 2,
                    Login = "Reader1", 
                    Password = "1423", 
                    Mail = "rEAr@gmail.com", 
                    Name = "Alex", 
                    Birthday = DateTime.Parse("18.04.1993"), 
                    DocumentType = "ID Passport",
                    DocumentNumber = 777789,
                    LibrarianLog = "Admin",
                    SecondName = "Marty"
                }
                );
            
            modelBuilder.Entity<DocumentType>().HasData(
                new DocumentType { ID = 1, Type = "Passport" },
                new DocumentType { ID = 2, Type = "ID Passport" },
                new DocumentType { ID = 3, Type = "Driver License" },
                new DocumentType { ID = 4, Type = "International Passport" }
                );
            
            modelBuilder.Entity<PublishingCodeType>().HasData(
                new PublishingCodeType { ID = 1, CodeType = 5},
                new PublishingCodeType { ID = 2, CodeType = 10},
                new PublishingCodeType { ID = 3, CodeType = 15},
                new PublishingCodeType { ID = 4, CodeType = 20},
                new PublishingCodeType { ID = 5, CodeType = 25}
                );

            modelBuilder.Entity<BookGenre>().HasData(
                new BookGenre { Genre = "Horror" },
                new BookGenre { Genre = "Novella" },
                new BookGenre { Genre = "Learning" },
                new BookGenre { Genre = "Fantasy" }
                );

            modelBuilder.Entity<Book>().HasData(
                new Book 
                {
                    ID = 1,
                    Name = "C# for smart", 
                    Genre = "Learning", 
                    Autor = "Oleg", 
                    Country = "Ukrain", 
                    City = "Kharkiv", 
                    PublishCodeType = 5, 
                    PublishCode = 655,
                    Count = 2,
                    ReturnDays = 14
                },
                new Book 
                { 
                    ID = 2,
                    Name = "World Story", 
                    Genre = "Learning", 
                    Autor = "Ivan", 
                    Country = "Ukrain", 
                    City = "Kiev", 
                    PublishCodeType = 5, 
                    PublishCode = 554,
                    Count = 1,
                    ReturnDays = 0
                },
                new Book 
                {
                    ID = 3,
                    Name = "Summer Time", 
                    Genre = "Novella", 
                    Autor = "Ivan", 
                    Country = "Ukrain", 
                    City = "Kiev", 
                    PublishCodeType = 10, 
                    PublishCode = 95,
                    Count = 5,
                },
                new Book 
                { 
                    ID = 4,
                    Name = "Mgla", 
                    Genre = "Horror", 
                    Autor = "Vasiliy", 
                    Country = "Poland", 
                    PublishCodeType = 15, 
                    PublishCode = 87,
                    Count = 2,
                    ReturnDays = 20
                }
                );
            modelBuilder.Entity<Autor>().HasData(
                new Autor { ID = 1, Name = "Oleg", LastName = "Fimov" },
                new Autor { ID = 2, Name = "Ivan", LastName = "Syropin", SecondName = "Grozniy" },
                new Autor { ID = 3, Name = "Vasiliy", LastName = "Syropin", SecondName = "Krot" }
            );

            base.OnModelCreating(modelBuilder);
        }
    }
}