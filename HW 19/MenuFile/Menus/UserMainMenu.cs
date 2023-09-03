using Librar.DAL;
using Librar.DAL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library
{
    internal class UserMainMenu
    {

        [MenuAction("Login", 1, "To Enter is system")]
        public void Login(DbContextOptionsBuilder<LibraryContext> optionBuilder)
        {
            Console.Clear();
            using var context = new LibraryContext(optionBuilder.Options);
            var librarians = context.Librarians.ToList();

            Console.WriteLine("Please enter Login");
            var login = Console.ReadLine();
            
            var currentLibrarian = librarians.FirstOrDefault(l => l.Login == login);

            if(currentLibrarian != null)
            {
                Console.WriteLine("Please enter your Password");
                var password = Console.ReadLine();
                if (password == currentLibrarian.Password)
                {
                    System(context);
                }
                else
                {
                    Console.WriteLine("Inccorect password");
                }
            }
            else
            {
                Console.WriteLine("There is no Users with this Login");
            }

            Console.WriteLine("Press any button to Continue");
            Console.ReadKey();
        }

        private void System(LibraryContext context)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("You are in system now");
            Console.ResetColor();



            Console.WriteLine("Press any button to Exit");
            Console.ReadKey();
        }

        [MenuSubmenu("Registration", 2, "Create new Librarian or Reader")]
        public Registration Registration { get; set; }
    }
    internal class Registration
    {
        [MenuAction("Librarian", 1, "Create new Librarian")]
        public void LibrarianRegistration(DbContextOptionsBuilder<LibraryContext> optionBuilder)
        {
            Console.Clear();
            using var context = new LibraryContext(optionBuilder.Options);
            var librarians = context.Librarians.ToList();

            Console.WriteLine("Please enter Login");
            var login = Console.ReadLine();

            if (login != null && login != string.Empty && !librarians.Any(l => l.Login == login))
            {
                Librarian librarian = new Librarian();
                librarian.Login = login;

                bool isExit = false;
                do
                {
                    Console.WriteLine("\nPlease Enter the Password");
                    var password = Console.ReadLine();

                    Console.WriteLine("Please repeat Password");
                    var passwordChek = Console.ReadLine();

                    if (password != null && password != string.Empty && password == passwordChek)
                    {
                        librarian.Password = password;
                        isExit = true;
                    }
                    else
                    {
                        Console.WriteLine("Passwords don`t match");
                    }


                } while (!isExit);

                isExit = false;
                
                do
                {
                    Console.WriteLine("Please Enter your Mail");
                    var mail = Console.ReadLine();

                    if (mail != null && mail != string.Empty)
                    {
                        librarian.Mail = mail;
                        isExit = true;
                    }
                    else
                    {
                        Console.WriteLine("Mail is incorrect or empty");
                    }


                } while (!isExit);

                context.Librarians.Add(librarian);
                context.SaveChanges();

                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("New Librarian was Add to base");
                Console.ResetColor();
            }
            else
            {
                Console.WriteLine("This Login is already in use");
            }


            context.Dispose();
            Console.WriteLine("Press any Key to continue");
            Console.ReadKey();
        }
        
        [MenuAction("Reader", 2, "Create new Reader")]
        public void ReaderRegistration(DbContextOptionsBuilder<LibraryContext> optionBuilder)
        {
            Console.Clear();
            using var context = new LibraryContext(optionBuilder.Options);
            var librarians = context.Readers.ToList();

            Console.WriteLine("Please enter Login");
            var login = Console.ReadLine();

            if (login != null && login != string.Empty && !librarians.Any(l => l.Login == login))
            {
                Reader reader = new Reader();
                reader.Login = login;

                bool isExit = false;
                do
                {
                    Console.WriteLine("\nPlease Enter the Password");
                    var password = Console.ReadLine();

                    Console.WriteLine("Please repeat Password");
                    var passwordChek = Console.ReadLine();

                    if (password != null && password != string.Empty && password == passwordChek)
                    {
                        reader.Password = password;
                        isExit = true;
                    }
                    else
                    {
                        Console.WriteLine("Passwords don`t match");
                    }


                } while (!isExit);

                isExit = false;

                do
                {
                    Console.WriteLine("Please Enter your Mail");
                    var mail = Console.ReadLine();

                    if (mail != null && mail != string.Empty)
                    {
                        reader.Mail = mail;
                        isExit = true;
                    }
                    else
                    {
                        Console.WriteLine("Mail is incorrect or empty");
                    }


                } while (!isExit);

                isExit = false;

                do
                {
                    Console.WriteLine("Please Enter your Name");
                    var name = Console.ReadLine();

                    if (name != null && name != string.Empty)
                    {
                        reader.Name = name;
                        isExit = true;
                    }
                    else
                    {
                        Console.WriteLine("Name is incorrect or empty");
                    }


                } while (!isExit);

                isExit = false;

                
                Console.WriteLine("Please Enter your LastName");
                reader.LastName = Console.ReadLine();
                        
                Console.WriteLine("Please Enter your Document Type ( Passport, ID Code, Driver license )");
                reader.DocumentType = Console.ReadLine();
                do
                {
                    try
                    {

                        Console.WriteLine("Please Enter your Document Number");
                        var documentNumber = int.Parse(Console.ReadLine());
                        reader.DocumentNumber = documentNumber;
                        isExit = true;
                    
                    }
                    catch (Exception ex) 
                    {
                        Console.WriteLine("Document Number is incorrect or empty"); 
                    }


                } while (!isExit);


                context.Readers.Add(reader);
                context.SaveChanges();

                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("New Librarian was Add to base");
                Console.ResetColor();
            }
            else
            {
                Console.WriteLine("This Login is already in use");
            }


            context.Dispose();
            Console.WriteLine("Press any Key to continue");
            Console.ReadKey();
        }
    }
}
