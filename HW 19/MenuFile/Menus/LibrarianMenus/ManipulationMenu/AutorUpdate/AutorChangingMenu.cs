using Librar.DAL;
using Librar.DAL.Interface;
using Librar.DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace Library
{
    internal class AutorChangingMenu
    {
        [MenuAction("Add", 1, "Add new Autor to library")]
        public void AddBook(DbContextOptionsBuilder<LibraryContext> optionBuilder)
        {
            Console.Write("Enter the new Autor Name: ");
            var newAutorName = Console.ReadLine();

            if (!string.IsNullOrEmpty(newAutorName))
            {
                using (var autors = new LibraryContext(optionBuilder.Options))
                {
                    AddNewAutor(autors, newAutorName);
                };
            }


        }

        private void AddNewAutor(LibraryContext context, string newBookName)
        {
            bool isCorrect = false;
            Autor newAutor = new() { Name = newBookName };
            do
            {

                Console.Write($"Please Enter Autors LastName ( May be Empty ): ");
                var lastName = Console.ReadLine();
                if (!string.IsNullOrEmpty(lastName))
                {
                    newAutor.LastName = lastName!;
                    isCorrect = true;
                }
                else Info.Error("Write correctly");


            } while (!isCorrect);

            do
            {
                isCorrect = false;
                Console.Write($"Please Enter Autors SecondName ( May be Empty ): ");
                var secondName = Console.ReadLine();
                if (!string.IsNullOrEmpty(secondName))
                {
                    newAutor.SecondName = secondName!;
                    isCorrect = true;
                }
                else Info.Error("Write correctly");


            } while (!isCorrect);

            do
            {
                isCorrect = false;
                Console.Write($"Please Enter Autors Birthday ( May be Empty ): ");

                if (DateTime.TryParse(Console.ReadLine(), out DateTime birthday))
                {
                    newAutor.Birthday = birthday!;
                    isCorrect = true;
                }
                else Info.Error("Write correctly");


            } while (!isCorrect);

        }

        [MenuAction("Change", 1, "Change Autor information")]
        public void ChangeAutor(DbContextOptionsBuilder<LibraryContext> optionBuilder)
        {
            using (var context = new LibraryContext(optionBuilder.Options))
            {
                SelectActionMenu.NamedList = context.Autors.ToList<IName>();
                Console.WriteLine("Write Autor Name ( Enter to see all )");
                var toFind = Console.ReadLine();

                if (!string.IsNullOrEmpty(toFind)) SelectActionMenu.NamedList.Where(x => x.Name.Contains(toFind, StringComparison.InvariantCultureIgnoreCase));

                SelectActionMenu.SearchToManipulate(() =>
                {
                    Info.InformKey($"Change {SelectActionMenu.Current.Name} information");

                    Menu.DetectMenu<AutorInformationUpdate>((Autor)SelectActionMenu.Current).Process();



                    context.Autors.Attach((Autor)SelectActionMenu.Current);
                    context.SaveChanges();
                    Info.SuccedKey($"Update succed");
                });
            };
        }

        [MenuAction("Remove", 3, "Remove Autor")]
        public void AutorRemove(DbContextOptionsBuilder<LibraryContext> optionBuilder)
        {
            using (var context = new LibraryContext(optionBuilder.Options))
            {
                SelectActionMenu.NamedList = context.Autors.ToList<IName>();
                Info.Inform("Write Autor Name ( Enter to see all )");
                var findAutor = Console.ReadLine();

                if (!string.IsNullOrEmpty(findAutor)) SelectActionMenu.NamedList.Where(x => x.Name.Contains(findAutor, StringComparison.OrdinalIgnoreCase));

                SelectActionMenu.SearchToManipulate(() =>
                {
                    Info.Inform($"Removing {SelectActionMenu.Current.Name} from base");
                    context.Autors.Remove((Autor)SelectActionMenu.Current);
                    SelectActionMenu.NamedList.RemoveAt(SelectActionMenu.Index);
                    context.SaveChanges();
                    Info.SuccedKey($"Removing succed");
                });
            };


        }
    }
}