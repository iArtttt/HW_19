using Librar.DAL;
using Librar.DAL.Models;
using Library.Common.Interface;
using Microsoft.EntityFrameworkCore;

namespace Library
{
    internal static class EnterInSystem
    {
        public static void Login(DbContextOptionsBuilder<LibraryContext> optionBuilder)
        {
            Console.Clear();
            using var context = new LibraryContext(optionBuilder.Options);
            List<IUser> users = context.Librarians.ToList<IUser>();
            users.AddRange(context.Readers.ToList<IUser>());

            Info.Inform("Please enter Login");
            var login = Console.ReadLine();
            
            var currentUser = users.FirstOrDefault(l => l.Login == login);

            if(currentUser != null)
            {
                Info.Inform("Please enter your Password");
                var password = Console.ReadLine();
                if (password == currentUser.Password)
                {
                    System(currentUser ,context);
                }
                else
                {
                    Info.ErrorKey("Inccorect password");
                }
            }
            else
            {
                Info.ErrorKey("There is no Users with this Login");
            }
        }

        private static void System(IUser currentUser, LibraryContext context)
        {
            Info.SuccedKey("You are in system now");

            if (currentUser is Librarian librarian) Menu.DetectMenu<LibrarianMainMenu>(librarian).Process();
            else if (currentUser is Reader reader)  Menu.DetectMenu<ReaderMainMenu>(reader).Process();

        }
    }
}
