using Librar.DAL.Models;

namespace Library
{
    internal class AutorInformationUpdate
    {
        [MenuAction("Name", 1)]
        public void ChangeName(Autor autor)
        {
            Console.Clear();
            Info.Inform($"If you do`t want to change name press ( Enter )");
            Info.Inform($"Autor Name is: {autor.Name}");

            var newName = Console.ReadLine();

            if (!string.IsNullOrEmpty(newName))
            {
                autor.Name = newName;
                Info.SuccedKey("Name was changed");
            }
        }
        [MenuAction("LastName", 2)]
        public void ChangeLastName(Autor autor)
        {
            Console.Clear();
            Info.Inform($"If you do`t want to change LastName press ( Enter )");
            Info.Inform($"Autor Name is: {autor.LastName}");

            var newLastName = Console.ReadLine();

            if (!string.IsNullOrEmpty(newLastName))
            {
                autor.LastName = newLastName;
                Info.SuccedKey("LastName was changed");
            }
        }
        [MenuAction("LastName", 2)]
        public void ChangeSecondName(Autor autor)
        {
            Console.Clear();
            Info.Inform($"If you do`t want to change SecondName press ( Enter )");
            Info.Inform($"Autor Name is: {autor.SecondName}");

            var newSecondName = Console.ReadLine();

            if (!string.IsNullOrEmpty(newSecondName))
            {
                autor.SecondName = newSecondName;
                Info.SuccedKey("SecondName was changed");
            }
        }
        [MenuAction("Birthday", 2)]
        public void ChangeBirthday(Autor autor)
        {
            Console.Clear();
            Info.Inform($"If you do`t want to change Birthday press ( Enter )");
            Info.Inform($"Autor Name is: {autor.Birthday}");
            try
            {
                var newBirthday = DateTime.Parse(Console.ReadLine());

                autor.Birthday = newBirthday;
                Info.SuccedKey("Birthday was changed");

            }
            catch (Exception)
            {
                Info.ErrorKey("Birthday was NOT changed");
            }
        }
    }
}