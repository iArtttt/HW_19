using Librar.DAL.Models;

namespace Library
{
    public class FullNameChange
    {
        [MenuAction("Name", 1)]
        public void NameChange(Reader reader)
        {
            Console.Clear();
            Info.Inform($"If you do`t want to change name press ( Enter )");
            Info.Inform($"Readers name is: {reader.Name}");

            var newName = Console.ReadLine();

            if (!string.IsNullOrEmpty(newName))
            {
                reader.Name = newName;
                Info.SuccedKey("Name was changed");
            }

        }
        
        [MenuAction("LastName", 2)]
        public void LastNameChange(Reader reader)
        {
            Console.Clear();
            Info.Inform($"If you do`t want to change Last Name press ( Enter )");
            Info.Inform($"Readers name is: {reader.LastName ??= "( ***[ Empty ]*** )"}");

            var newLastName = Console.ReadLine();

            if (!string.IsNullOrEmpty(newLastName))
            {
                reader.LastName = newLastName;
                Info.SuccedKey("LastName was changed");
            }

        }
        
        [MenuAction("SecondName", 3)]
        public void SecondNameChange(Reader reader)
        {
            Console.Clear();
            Info.Inform($"If you do`t want to change Second Name press ( Enter )");
            Info.Inform($"Readers name is: {reader.SecondName ??="( ***[ Empty ]*** )"}");

            var newSecondName = Console.ReadLine();

            if (!string.IsNullOrEmpty(newSecondName))
            {
                reader.SecondName = newSecondName;
                Info.SuccedKey("SecondName was changed");
            }

        }
        
        [MenuAction("Birthday", 4)]
        public void BirthdayChange(Reader reader)
        {
            Console.Clear();
            Info.Inform($"If you do`t want to change Birthday press ( Enter )");
            Info.Inform($"Format: YYYY,MM,DD");
            Info.Inform($"Readers name is: {reader.Birthday}");

            var newBirthday = Console.ReadLine();
            try
            {

                if (!string.IsNullOrEmpty(newBirthday))
                {
                    reader.Birthday = DateTime.Parse(newBirthday);
                    Info.SuccedKey("Birthday was changed");
                }
            }
            catch 
            {
                Info.ErrorKey("Incorrect Symbols");
            }

        }
    }
}