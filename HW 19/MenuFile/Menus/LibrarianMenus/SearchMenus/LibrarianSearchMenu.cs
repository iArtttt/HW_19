using Librar.DAL;
using Librar.DAL.Interface;
using Librar.DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace Library
{
    internal class LibrarianSearchMenu
    {

        [MenuAction("Search Book", 1)]
        public void SearchByBook(DbContextOptionsBuilder<LibraryContext> optionBuilder)
        {
            Console.Clear();
            Info.Inform("Write Autor you search ( Enter to see all )");
            var search = Console.ReadLine();

            Info.Inform("Searching...");

            using (var bo = new LibraryContext(optionBuilder.Options))
            {
                if (!string.IsNullOrEmpty(search))
                    SelectActionMenu.NamedList = bo.Books.Where(n => n.Name.Contains(search, StringComparison.InvariantCultureIgnoreCase)).ToList<IName>();
                else
                    SelectActionMenu.NamedList = bo.Books.ToList<IName>();

                SelectActionMenu.SearchToManipulate(() =>
                {
                    var book = (Book)SelectActionMenu.Current;
                    Info.Inform($"Name:\t{ book.Name }" +
                        $"\nGenre:\t{book.Genre}" +
                        $"\nCountry:{book.Country}" +
                        $"\nCity:\t{book.City}" +
                        $"\nAutor:\t{book.AutorNavigation}" +
                        $"\nCount:\t{book.Count}");
                }
                );

            }
        }
        [MenuAction("Search Autor", 1)]
        public void SearchByAutor(DbContextOptionsBuilder<LibraryContext> optionBuilder)
        {
            Console.Clear();
            Info.Inform("Write Autor you search ( Enter to see all )");
            var search = Console.ReadLine();

            Info.Inform("Searching...");

            using (var bo = new LibraryContext(optionBuilder.Options))
            {
                if (!string.IsNullOrEmpty(search))
                    SelectActionMenu.NamedList = bo.Autors.Where(n => n.Name.Contains(search, StringComparison.InvariantCultureIgnoreCase)).ToList<IName>();
                else
                    SelectActionMenu.NamedList = bo.Autors.ToList<IName>();

                SelectActionMenu.SearchToManipulate(() =>
                {
                    var autor = (Autor)SelectActionMenu.Current;
                    Info.Inform(autor.ToString());
                    Info.Inform($"{string.Join("\n",  autor.Books
                        .ToArray()
                        .Select(b => "Name: " + b.Name + "|| Genre: " + b.Genre + " || Country,City: " + b.Country + ", " + b.City + " || Count: " + b.Count)
                        )}");
                }
                );

            }
        }

    }
}