using Librar.DAL;
using Librar.DAL.Models;
using Library.Common.Interface;
using Microsoft.EntityFrameworkCore;

namespace Library
{
    internal class BooksChangingMenu
    {
        [MenuAction("Add", 1, "Add book to library")]
        public void AddBook(DbContextOptionsBuilder<LibraryContext> optionBuilder)
        {
            Console.WriteLine("Enter the book Name");
            var newBookName = Console.ReadLine();

            if(!string.IsNullOrEmpty(newBookName))
            {
                using (var books = new LibraryContext(optionBuilder.Options))
                {
                    var book = books.Books.Where(n => n.Name == newBookName);
                    if(book != null)
                    {
                        Info.Inform("Book with this name is alreary in Library");
                        Info.Inform("Do you want add to existing ones? ( Y, + to add )");
                        
                        var toAdd = Console.ReadLine().ToLower();
                        
                        if(toAdd == "y" || toAdd == "+")
                        {
                            SelectActionMenu.NamedList = book.ToList<IName>();
                            SelectActionMenu.SearchToManipulate(() =>
                            {
                                var currentBook = (Book)SelectActionMenu.Current;
                                Info.Inform($"You want add book to this autor? ( Y, + ) if yes");
                                Info.Inform($"{currentBook.AutorNavigation}");

                                var toAdd = Console.ReadLine().ToLower();
                                if (toAdd == "y" || toAdd == "+")
                                {
                                    books.Attach(currentBook);
                                    currentBook.Count++;
                                    
                                    Info.Succed("Book was Add to Library");
                                }

                            });
                        }
                        else 
                            AddNewBook(books, newBookName);
                        
                    }
                    else 
                    {

                        AddNewBook(books, newBookName);

                    }
                    books.SaveChanges();
                };
            }


        }

        private void AddNewBook(LibraryContext books, string newBookName)
        {
            bool isCorrect = false;
            Book newBook = new() { Name = newBookName };
            do
            {

                Console.WriteLine($"Please Enter Book Genre: ( {string.Join(", ", books.BookGenres.Select(g => g.Genre).ToArray())} )");
                var genr = Console.ReadLine();
                if (books.BookGenres.Any(g => g.Genre == genr))
                {
                    newBook.Genre = genr!;
                    isCorrect = true;
                }
                else Info.Error("Write correctly");


            } while (!isCorrect);

            do
            {
                isCorrect = false;
                Console.WriteLine($"Please Enter Book Publishing Code Type: ( {string.Join(", ", books.PublishingCodeTypes.Select(g => g.CodeType).ToArray())} )");
                
                if (int.TryParse(Console.ReadLine(), out int codeType) && books.PublishingCodeTypes.Any(g => g.CodeType == codeType))
                {
                    newBook.PublishCodeType = codeType!;
                    isCorrect = true;
                }
                else Info.Error("Write correctly");


            } while (!isCorrect);

            do
            {
                isCorrect = false;
                Console.WriteLine($"Please Enter Book Publish Code ( Number )");
                
                if (int.TryParse(Console.ReadLine(), out int publishCode))
                {
                    newBook.PublishCode = publishCode!;
                    isCorrect = true;
                }
                else Info.Error("Write correctly");


            } while (!isCorrect);

            do
            {
                isCorrect = false;
                Console.WriteLine($"Please Enter Book Autor: {string.Join(", ", books.Autors.Select(g => g.Name).ToArray())}");
                var autor = Console.ReadLine();
                if (books.Autors.Any(g => g.Name == autor))
                {
                    newBook.Autor = autor!;
                    isCorrect = true;
                }
                else Info.Error("Write correctly");


            } while (!isCorrect);

            Console.WriteLine("How many books do you want give to library?");

            if (int.TryParse(Console.ReadLine(), out int count))
                newBook.Count = count;
            else
                newBook.Count = 1;
            books.Add(newBook);

            Console.WriteLine("For how long ( Days ) you allow to borrow this book? ( Enter for 30 )");

            if (int.TryParse(Console.ReadLine(), out int days))
                newBook.ReturnDays = count;
            else
                newBook.ReturnDays = 30;
            books.Add(newBook);
        }

        [MenuAction("Change", 1, "Change book information")]
        public void ChangeBook(DbContextOptionsBuilder<LibraryContext> optionBuilder)
        {
            using (var context = new LibraryContext(optionBuilder.Options))
            {
                SelectActionMenu.NamedList = context.Books.ToList<IName>();
                Console.WriteLine("Write Books Name ( Enter to see all )");
                var toFind = Console.ReadLine();

                if (!string.IsNullOrEmpty(toFind)) SelectActionMenu.NamedList.Where(x => x.Name.Contains(toFind, StringComparison.InvariantCultureIgnoreCase));

                SelectActionMenu.SearchToManipulate(() =>
                {
                    Info.InformKey($"Change {SelectActionMenu.Current.Name} information");

                    Menu.DetectMenu<BookInformationUpdate>((Book)SelectActionMenu.Current, context.BookGenres.ToList(), context.Autors.ToList()).Process();



                    context.Books.Attach((Book)SelectActionMenu.Current);
                    context.SaveChanges();
                    Info.SuccedKey($"Update succed");
                });
            };
        }

        [MenuAction("Remove", 3, "Remove Book")]
        public void BookRemove(DbContextOptionsBuilder<LibraryContext> optionBuilder)
        {
            using (var context = new LibraryContext(optionBuilder.Options))
            {
                SelectActionMenu.NamedList = context.Books.ToList<IName>();
                Info.Inform("Write Books Name ( Enter to see all )");
                var findBook = Console.ReadLine();

                if (!string.IsNullOrEmpty(findBook)) SelectActionMenu.NamedList.Where(x => x.Name.Contains(findBook, StringComparison.OrdinalIgnoreCase));

                SelectActionMenu.SearchToManipulate(() =>
                {
                    Info.Inform($"Removing {SelectActionMenu.Current.Name} from base");
                    context.Books.Remove((Book)SelectActionMenu.Current);
                    SelectActionMenu.NamedList.RemoveAt(SelectActionMenu.Index);
                    context.SaveChanges();
                    Info.SuccedKey($"Removing succed");
                });
            };


        }
    }
}