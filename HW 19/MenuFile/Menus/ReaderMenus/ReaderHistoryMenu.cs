using Librar.DAL;
using Librar.DAL.Interface;
using Librar.DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace Library
{
    internal class ReaderHistoryMenu
    {
        [MenuAction("To Return", 1, "Books you need return")]
        public void ToReturn(DbContextOptionsBuilder<LibraryContext> optionBuilder, Reader reader)
        {
            using (var context = new LibraryContext(optionBuilder.UseLazyLoadingProxies(true).Options))
            {
                SelectActionMenu.NamedList = reader.BorrowedBooks
                    .Where(b => !b.IsReturned)
                    .OrderBy(d => d.ReturneTo)
                    .Select(b => b.Book)
                    .ToList<IName>();

                SelectActionMenu.SearchToManipulate(() =>
                {
                    var book = (Book)SelectActionMenu.Current;

                    Info.Inform("Do you want return this book?");
                    var borrowedBook = reader.BorrowedBooks.FirstOrDefault(b => b.Book == book);
                    
                    if (borrowedBook.ReturneTo <= DateTime.Now)
                        Info.Error($"\t**( Overdue )**\n|| Name: {book.Name}\n|| Today Is: {DateTime.Today}\n|| ReturneTo: {borrowedBook.ReturneTo}");
                    else
                        Info.Inform($"|| Name: {book.Name}\n|| Taked: {borrowedBook.Taked}\n|| Today Is: {DateTime.Today}|| ReturneTo: {borrowedBook.ReturneTo}");

                    Return(context, reader, borrowedBook);

                    context.SaveChanges();
                });

            }
        }
        
        [MenuAction("All Books", 2, "All Books you holds")]
        public void AllBooks(DbContextOptionsBuilder<LibraryContext> optionBuilder, Reader reader)
        {
            using (var context = new LibraryContext(optionBuilder.UseLazyLoadingProxies(true).Options))
            {
                SelectActionMenu.NamedList = reader.BorrowedBooks
                    .OrderBy(d => d.IsReturned)
                    .ThenBy(d => d.ReturneTo)
                    .Select(b => b.Book)
                    .ToList<IName>();

                SelectActionMenu.SearchToManipulate(() =>
                {
                    var book = (Book)SelectActionMenu.Current;
                    var borrowedBook = reader.BorrowedBooks.FirstOrDefault(b => b.Book == book);

                    if (!borrowedBook.IsReturned)
                    {

                        Info.Inform("Do you want return this book?");

                        if (borrowedBook.ReturneTo <= DateTime.Now)
                            Info.Error($"\t**( Overdue )**\n|| Name: {book.Name}\n|| Taked: {borrowedBook.Taked}\n|| Today Is: {DateTime.Now}\n|| ReturneTo: {borrowedBook.ReturneTo}");
                        else
                            Info.Inform($"|| Name: {book.Name}\n|| Taked: {borrowedBook.Taked}\n|| Today Is: {DateTime.Now}\n|| ReturneTo: {borrowedBook.ReturneTo}");
                        Return(context, reader, borrowedBook);
                    }
                    else
                    {
                        if (borrowedBook.WasOverdue == true)
                            Info.ErrorKey($"{borrowedBook.StoryBack}");
                        else
                            Info.InformKey($"{borrowedBook.StoryBack}");
                    }
                    context.SaveChanges();

                });

            }
        }

        private void Return(LibraryContext context, Reader reader, BorrowedBooks borrowedBook)
        {

            Info.Inform($"Do you want to return it now? ( y, + ) to return");

            var toReturn = Console.ReadLine().ToLower();

            if (!string.IsNullOrEmpty(toReturn) && toReturn == "y" || toReturn == "+")
            {
                context.Books.Attach(borrowedBook.Book);
                context.BorrowedBooks.Attach(borrowedBook);

                borrowedBook.IsReturned = true;
                if(borrowedBook.ReturneTo <= DateTime.Now)
                {
                    borrowedBook.StoryBack = $"{reader.Name}, return book ( {borrowedBook.Book.Name} ) || **[ Overdue ]** Return Data : {DateTime.Now}";
                    borrowedBook.WasOverdue = true;
                }
                else
                {
                    borrowedBook.StoryBack = $"{reader.Name}, return book ( {borrowedBook.Book.Name} ) || Return Data : {DateTime.Now}";
                    borrowedBook.WasOverdue = false;
                }
                borrowedBook.Book.Count++;

                

                Info.SuccedKey($"Book {borrowedBook.Book.Name} was returned");
            }

        }
    }
}