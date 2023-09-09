using Librar.DAL;
using Librar.DAL.Interface;
using Librar.DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace Library
{
    internal class LibrarianReaderManipulationMenu
    {
        //[MenuAction("Librarian", 1, "Create new Librarian")]
        //public void LibrarianRegistration(DbContextOptionsBuilder<LibraryContext> optionBuilder)
        //{
        //    Console.Clear();

        //    using var context = new LibraryContext(optionBuilder.Options);
        //    List<IEntry> users = context.Librarians.ToList<IEntry>();
        //    users.AddRange(context.Readers.ToList<IEntry>());

        //    Librarian newLibrarian = new Librarian();
        //    var result = EntryRegister(newLibrarian, users);

        //    if (result != null)
        //    {
        //        newLibrarian = (Librarian)result;

        //        context.Librarians.Add(newLibrarian);
        //        context.SaveChanges();

        //        Console.ForegroundColor = ConsoleColor.Green;
        //        Console.WriteLine("New Librarian was Add to base");
        //        Console.ResetColor();
        //    }
        //    else
        //    {
        //        Console.WriteLine("");
        //    }

        //    Console.WriteLine("Press any Key to continue");
        //    Console.ReadKey();
        //}

        

        [MenuAction("Add", 1, "Create new Reader")]
        public void ReaderRegistration(DbContextOptionsBuilder<LibraryContext> optionBuilder, Librarian currentLibrarian)
        {
            Console.Clear();
            using var context = new LibraryContext(optionBuilder.Options);
            List<IEntry> users = context.Librarians.ToList<IEntry>();
            users.AddRange(context.Readers.ToList<IEntry>());

            Reader newReader = new Reader();
            IEntry? result = EntryRegister(newReader, users);


            if (result != null)
            {
                newReader = (Reader)PersonRegister((Reader)result);

                
                
                bool isExit = false;
                do
                {
                    try
                    {
                        Console.WriteLine($"Please Enter your Document Type ( {string.Join(", ", context.DocumentTypes.Select(t => t.Type))} )");
                        var documentType = Console.ReadLine();

                        if (!string.IsNullOrEmpty(documentType) && context.DocumentTypes.Any(t => t.Type == documentType))
                        {
                            newReader.DocumentType = documentType!;
                            isExit = true;
                        }
                        else
                        {
                            Info.Error("Please write correct document type");
                        }

                    }
                    catch (Exception ex)
                    {
                        Info.Error("Document Type is incorrect or empty");
                    }


                } while (!isExit);

                isExit = false;
                do
                {
                    try
                    {

                        Console.WriteLine("Please Enter your Document Number");
                        var documentNumber = int.Parse(Console.ReadLine());
                        newReader.DocumentNumber = documentNumber;
                        isExit = true;

                    }
                    catch (Exception ex)
                    {
                        Info.Error("Document Number is incorrect or empty");
                    }


                } while (!isExit);

                newReader.LibrarianLog = currentLibrarian.Login;

                context.Readers.Add(newReader);
                context.SaveChanges();

                Info.SuccedKey("New Reader was Add to base");
            }
        }
        
        [MenuAction("Update", 2, "Update Reader")]
        public void UpdateReader(DbContextOptionsBuilder<LibraryContext> optionBuilder)
        {
            using (var readers = new LibraryContext(optionBuilder.Options))
            {
                SelectActionMenu.NamedList = readers.Readers.ToList<IName>();
                Console.WriteLine("Write Reader Name ( Enter to see all )");
                var findReader = Console.ReadLine();

                if (!string.IsNullOrEmpty(findReader)) SelectActionMenu.NamedList.Where(x => x.Name.Contains(findReader, StringComparison.OrdinalIgnoreCase));

                SelectActionMenu.SearchToManipulate(() =>
                {
                    Info.InformKey($"Update {SelectActionMenu.Current.Name} information");

                    Menu.DetectMenu<ReaderInformationUpdate>((Reader)SelectActionMenu.Current).Process();



                    readers.Readers.Attach((Reader)SelectActionMenu.Current);
                    readers.SaveChanges();
                    Info.SuccedKey($"Update succed");
                });
            };
        }

        [MenuAction("Remove", 3, "Remove Reader")]
        public void ReaderRemove(DbContextOptionsBuilder<LibraryContext> optionBuilder)
        {
            using (var readers = new LibraryContext(optionBuilder.Options))
            {
                SelectActionMenu.NamedList = readers.Readers.ToList<IName>();
                Info.Inform("Write Reader Name ( Enter to see all )");
                var findReader = Console.ReadLine();

                if (!string.IsNullOrEmpty(findReader)) SelectActionMenu.NamedList.Where(x => x.Name == findReader);

                SelectActionMenu.SearchToManipulate(() =>
                {
                    Info.Inform($"Removing {SelectActionMenu.Current.Name} from base");
                    readers.Readers.Remove((Reader)SelectActionMenu.Current);
                    SelectActionMenu.NamedList.RemoveAt(SelectActionMenu.Index);
                    readers.SaveChanges();
                    Info.SuccedKey($"Removing succed");
                });
            };
        

        }
        
        private IEntry? EntryRegister(IEntry entry, List<IEntry> entries)
        {
            Console.WriteLine("Please enter Login");
            var login = Console.ReadLine();

            if (login != null && login != string.Empty && !entries.Any(l => l.Login == login))
            {
                entry.Login = login;

                bool isExit = false;
                do
                {
                    Info.Inform("\nPlease Enter the Password");
                    var password = Console.ReadLine();

                    Info.Inform("Please repeat Password");
                    var passwordChek = Console.ReadLine();

                    if (password != null && password != string.Empty && password == passwordChek)
                    {
                        entry.Password = password;
                        isExit = true;
                    }
                    else
                    {
                        Info.Error("Passwords don`t match");
                    }


                } while (!isExit);

                isExit = false;

                do
                {
                    Console.WriteLine("Please Enter your Mail");
                    var mail = Console.ReadLine();

                    if (mail != null && mail != string.Empty)
                    {
                        entry.Mail = mail;
                        isExit = true;
                    }
                    else
                    {
                        Info.Error("Mail is incorrect or empty");
                    }


                } while (!isExit);

            }
            else
            {
                Info.Error("This Login is already in use");
                return null;
            }
            return entry;
        }
        private IPerson PersonRegister(IPerson person)
        {
            bool isExit = false;
            do
            {
                Console.WriteLine("Please Enter your Name");
                var name = Console.ReadLine();

                if (name != null && name != string.Empty)
                {
                    person.Name = name;
                    isExit = true;
                }
                else
                {
                    Info.Error("Name is incorrect or empty");
                }


            } while (!isExit);

            isExit = false;


            Console.WriteLine("Please Enter your LastName ( May be Empty )");
            person.LastName = Console.ReadLine();

            Console.WriteLine("Please Enter your SecondName ( May be Empty )");
            person.SecondName = Console.ReadLine();

            Console.WriteLine("Please Enter your Birthday ( May be Empty )");
            person.Birthday = DateTime.TryParse(Console.ReadLine(), out DateTime dateTime) ? dateTime : null;

            return person;

        }

    }
}
