using Librar.DAL;
using Librar.DAL.Interface;
using Librar.DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace Library
{
    internal class LibrarianInformationMenu
    {
        [MenuAction("Debtors", 1, "Debtors and their debt")]
        public void Debtors(DbContextOptionsBuilder<LibraryContext> optionBuilder)
        {
            using (var context = new LibraryContext(optionBuilder.UseLazyLoadingProxies(true).Options))
            {
                List<Reader> readers = new ();

                var debtors = context.BorrowedBooks
                    .Where(b => !b.IsReturned && b.ReturneTo <= DateTime.Now)
                    .OrderBy(b => b.ReaderID)
                    .ThenBy(d => d.ReturneTo)
                    .ToArray();
                if (debtors.Length > 0)
                {

                    foreach (var debtor in debtors)
                    {
                        if(!readers.Any(i => i.Id == debtor.Reader.Id))
                        {
                            Info.Inform($"ID: {debtor.Reader.Id} => {debtor.Reader.Name} {debtor.Reader.LastName} {debtor.Reader.SecondName}");
                            readers.Add(debtor.Reader);
                        }
                        Info.Inform($"\tBook ID: {debtor.BookID} => Name: {debtor.Book.Name} \t||\t ReturneTo: {debtor.ReturneTo}\t||\tAutor {debtor.Book.Autor}");
                    }
                    Info.InformKey("There is`t more Debtors");
                }
                else
                    Info.SuccedKey("There is`t any Debtors");
                

            }

        }

        [MenuAction("All Book holders", 2, "Readers who holding books")]
        public void AllDebtors(DbContextOptionsBuilder<LibraryContext> optionBuilder)
        {
            using (var context = new LibraryContext(optionBuilder.UseLazyLoadingProxies(true).Options))
            {
                List<Reader> readers = new ();

                var debtors = context.BorrowedBooks
                    .Where(b => !b.IsReturned)
                    .OrderBy(b => b.ReaderID)
                    .ThenBy(d => d.ReturneTo)
                    .ToArray();
                if (debtors.Length > 0)
                {

                    foreach (var debtor in debtors)
                    {
                        if(!readers.Any(i => i.Id == debtor.Reader.Id))
                        {
                            Info.Inform($"ID: {debtor.Reader.Id} => {debtor.Reader.Name} {debtor.Reader.LastName} {debtor.Reader.SecondName}");
                            readers.Add(debtor.Reader);
                        }
                        if (debtor.ReturneTo <= DateTime.Now)
                            Info.Error($"\tBook ID: {debtor.BookID} => Name: {debtor.Book.Name} \t||\t ReturneTo: {debtor.ReturneTo}\t||\tAutor {debtor.Book.Autor}");
                        else
                            Info.Inform($"\tBook ID: {debtor.BookID} => Name: {debtor.Book.Name} \t||\t ReturneTo: {debtor.ReturneTo}\t||\tAutor {debtor.Book.Autor}");
                    }
                    Info.InformKey("There is`t more Readers");
                }
                else
                    Info.SuccedKey("No one reading");               

            }

        }

        [MenuAction("Reader Story", 3, "Select reader and see them Story")]
        public void Story(DbContextOptionsBuilder<LibraryContext> optionBuilder)
        {
            using (var context = new LibraryContext(optionBuilder.UseLazyLoadingProxies(true).Options))
            {

                SelectActionMenu.NamedList = context.Readers.ToList<IName>();


                SelectActionMenu.SearchToManipulate(() =>
                {
                    var reader = (Reader)SelectActionMenu.Current;
                    var borrowedBooksStory = reader.BorrowedBooks.OrderBy(b => !b.IsReturned).ToArray();
                    int overdues = 0;


                    if (borrowedBooksStory.Length > 0)
                    {

                        foreach (var borrowedBook in borrowedBooksStory)
                        {

                            if (!borrowedBook.IsReturned)
                            {
                                if (borrowedBook.ReturneTo <= DateTime.Now)
                                {
                                    Info.Error($"**[ Overdue ]**");
                                    Info.Error(borrowedBook.StoryTook);
                                    overdues++;
                                }
                                else
                                    Info.Inform(borrowedBook.StoryTook);


                            }
                            else
                            {
                                if (borrowedBook.WasOverdue)
                                {
                                    Info.Error($"**[ Overdue ]**");
                                    Info.Error(borrowedBook.StoryTook);
                                    Info.Error(borrowedBook.StoryBack);
                                    overdues++;
                                }
                                else
                                {
                                    Info.Inform(borrowedBook.StoryTook);
                                    Info.Inform(borrowedBook.StoryBack);
                                }

                            }
                            Console.WriteLine();
                        }
                        Info.Error($"Overdues: {overdues}");
                        Info.InformKey("This is all reader story");
                    }
                    else
                        Info.ErrorKey("This reader has`t story");
                });               
                

            }

        }

       
    }
}