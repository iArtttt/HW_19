using Librar.DAL.Interface;
using Librar.DAL.Models;
using Librar.DAL;
using Microsoft.EntityFrameworkCore;

namespace Library
{
    internal class ReaderSearchMenu
    {
        [MenuAction("Search Book", 1, "Find book you want")]
        public void SearchByBook(DbContextOptionsBuilder<LibraryContext> optionBuilder, Reader reader)
        {
            Console.Clear();
            Info.Inform("Write Autor you search ( Enter to see all )");
            var search = Console.ReadLine();

            Info.Inform("Searching...");

            using (var bo = new LibraryContext(optionBuilder.UseLazyLoadingProxies().Options))
            {
                if (!string.IsNullOrEmpty(search))
                    SelectActionMenu.NamedList = bo.Books
                        .Where(b => b.Count > 0 && b.Name
                        .Contains(search, StringComparison.InvariantCultureIgnoreCase))
                        .ToList<IName>();
                else
                    SelectActionMenu.NamedList = bo.Books.Where(b => b.Count > 0).ToList<IName>();

                SelectActionMenu.SearchToManipulate(() =>
                {
                    var book = (Book)SelectActionMenu.Current;
                    Info.Inform($"Name:\t{book.Name}" +
                        $"\nGenre:\t{book.Genre}" +
                        $"\nCountry:{book.Country}" +
                        $"\nCity:\t{book.City}" +
                        $"\nAutor:\t{book.AutorNavigation}");

                    Take(bo, reader, book);

                }
                );

            }
        }

        private void Take(LibraryContext context, Reader reader, Book book)
        {
            Console.WriteLine();
            if (book.Count > 0)
            {

                Info.Inform("Do you want take this book? ( y, + ) to take");
                var wantTake = Console.ReadLine().ToLower();

                if (!string.IsNullOrEmpty(wantTake) && (wantTake == "y" || wantTake == "+"))
                {
                    BorrowedBook borrowBook = new();
                    var returnTo = DateTime.Now.AddDays(book.ReturnDays);

                    borrowBook.Name = book.Name;
                    borrowBook.ReaderID = reader.ID;
                    borrowBook.BookID = book.ID;
                    borrowBook.Taked = DateTime.Now;
                    borrowBook.ReturneTo = returnTo;
                    borrowBook.StoryTook = $"{reader.Name}, take book ( {book.Name} ) for: {returnTo}";
                    borrowBook.IsReturned = false;

                    context.BorrowedBooks.Add(borrowBook);

                    context.Attach(book);
                    book.Count--;
                    context.SaveChanges();
                    Info.SuccedKey($"You take book: {book.Name}, you have {book.ReturnDays} days to return it");
                    context.Dispose();
                }
            }
            else
                Info.ErrorKey("Sorry, someone just took the last copy");
        }

        [MenuAction("Search Autor", 2, "Find book from your favorite autor")]
        public void SearchByAutor(DbContextOptionsBuilder<LibraryContext> optionBuilder, Reader reader)
        {
            Console.Clear();
            Info.Inform("Write Autor you search ( Enter to see all )");
            var search = Console.ReadLine();

            Info.Inform("Searching...");

            using (var bo = new LibraryContext(optionBuilder.UseLazyLoadingProxies().Options))
            {
                if (!string.IsNullOrEmpty(search))
                    SelectActionMenu.NamedList = bo.Autors.Where(n => n.Name.Contains(search, StringComparison.InvariantCultureIgnoreCase)).ToList<IName>();
                else
                    SelectActionMenu.NamedList = bo.Autors.ToList<IName>();

                SelectActionMenu.SearchToManipulate(() =>
                {
                    var autor = (Autor)SelectActionMenu.Current;

                    var books = autor.Books.Where(b => b.Count > 0).ToArray();

                    Info.Inform(autor.ToString());
                    
                    Info.Inform($"{string.Join("\n", books
                        .Select(b => "Name: " + b.Name + "|| Genre: " + b.Genre + " || Country,City: " + b.Country + ", " + b.City)
                        )}");

                    Console.WriteLine();

                    Info.Inform($"Do you want take one of this books?( y, + ) to take");
                    var wantTake = Console.ReadLine().ToLower();

                    if (!string.IsNullOrEmpty(wantTake) && (wantTake == "y" || wantTake == "+"))
                    {
                        SelectActionMenu.NamedList = books.ToList<IName>();
                        SelectActionMenu.SearchToManipulate(() =>
                        {
                            var book = (Book)SelectActionMenu.Current;
                            Info.Inform($"Name:\t{book.Name}" +
                                $"\nGenre:\t{book.Genre}" +
                                $"\nCountry:{book.Country}" +
                                $"\nCity:\t{book.City}" +
                                $"\nAutor:\t{book.AutorNavigation}");

                            Take(bo, reader, book);
                        });
                    }
                }
                );

            }
        }

    }
}
