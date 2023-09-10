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
            using (var context = new LibraryContext(optionBuilder.UseLazyLoadingProxies(false).Options))
            {
                SelectActionMenu.NamedList = reader.BorrowedBooks
                    .Where(b => !b.IsReturned)
                    .OrderBy(d => d.ReturneTo)
                    .ToList<IName>();

                SelectActionMenu.SearchToManipulate(() =>
                {
                    var borrowedBook = (BorrowedBook)SelectActionMenu.Current;


                    Info.Inform("Do you want return this book?");
                    
                    if (borrowedBook!.ReturneTo <= DateTime.Now)
                        Info.Error($"\t**( Overdue )**\n|| Today Is: {DateTime.Today}\n|| ReturneTo: {borrowedBook.ReturneTo}");
                    else
                        Info.Inform($"|| Taked: {borrowedBook.Taked}\n|| Today Is: {DateTime.Today}|| ReturneTo: {borrowedBook.ReturneTo}");

                    Return(context, reader, borrowedBook);

                    context.SaveChanges();
                });

            }
        }
        
        [MenuAction("All Books", 2, "All Books you holds")]
        public void AllBooks(DbContextOptionsBuilder<LibraryContext> optionBuilder, Reader reader)
        {
            using (var context = new LibraryContext(optionBuilder.UseLazyLoadingProxies(false).Options))
            {
                SelectActionMenu.NamedList = reader.BorrowedBooks
                    .OrderBy(d => d.IsReturned)
                    .ThenBy(d => d.ReturneTo)
                    .ToList<IName>();

                SelectActionMenu.SearchToManipulate(() =>
                {
                    var borrowedBook = (BorrowedBook)SelectActionMenu.Current;

                    if (!borrowedBook!.IsReturned)
                    {

                        Info.Inform("Do you want return this book?");

                        if (borrowedBook.ReturneTo <= DateTime.Now)
                            Info.Error($"\t**( Overdue )**\n|| Taked: {borrowedBook.Taked}\n|| Today Is: {DateTime.Now}\n|| ReturneTo: {borrowedBook.ReturneTo}");
                        else
                            Info.Inform($"|| Taked: {borrowedBook.Taked}\n|| Today Is: {DateTime.Now}\n|| ReturneTo: {borrowedBook.ReturneTo}");
                        Return(context, reader, borrowedBook);
                    }
                    else
                    {
                        if (borrowedBook.WasOverdue)
                            Info.ErrorKey($"{borrowedBook.StoryBack}");
                        else
                            Info.InformKey($"{borrowedBook.StoryBack}");
                    }
                    context.SaveChanges();

                });

            }
        }

        private void Return(LibraryContext context, Reader reader, BorrowedBook borrowedBook)
        {
            if (!borrowedBook.IsReturned)
            {

                Info.Inform($"Do you want to return it now? ( y, + ) to return");

                var toReturn = Console.ReadLine().ToLower();

                if (!string.IsNullOrEmpty(toReturn) && toReturn == "y" || toReturn == "+")
                {
                    context.Books.Attach(borrowedBook.Book);
                    context.BorrowedBooks.Attach(borrowedBook);

                    borrowedBook.IsReturned = true;
                    if (borrowedBook.ReturneTo <= DateTime.Now)
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
            else
                Info.SuccedKey("This book is already reterned");

        }
    }
}