using Librar.DAL.Models;
using static System.Reflection.Metadata.BlobBuilder;

namespace Library
{
    internal class BookInformationUpdate
    {
        [MenuAction("Name", 1)]
        public void ChangeName(Book book)
        {
            Console.Clear();
            Info.Inform($"If you do`t want to change name press ( Enter )");
            Info.Inform($"Book Name is: {book.Name}");

            var newName = Console.ReadLine();

            if (!string.IsNullOrEmpty(newName))
            {
                book.Name = newName;
                Info.SuccedKey("Name was changed");
            }
        }
        [MenuAction("Genre", 2)]
        public void ChangeGenre(Book book, List<BookGenre> genres)
        {
            Console.Clear();
            Info.Inform($"If you do`t want to change Genre press ( Enter )");
            Info.Inform($"Genres: ( {string.Join(", ", genres.Select(g => g.Genre))} )");
            Info.Inform($"Book Genre is: {book.Genre}");

            var newGenre = Console.ReadLine();

            if (!string.IsNullOrEmpty(newGenre) && genres.Any(g => g.Genre == newGenre))
            {
                book.Genre = newGenre;
                Info.SuccedKey("Genre was changed");
            }
        }
        [MenuAction("Autor", 3)]
        public void ChangeAutor(Book book, List<Autor> autors)
        {
            Console.Clear();
            Info.Inform($"If you do`t want to change name press ( Enter )");
            Info.Inform($"Genres: ( {string.Join(", ", autors.Select(g => g.Name))} )");
            Info.Inform($"Book Autor is: {book.Autor}");

            var newAutor = Console.ReadLine();

            if (!string.IsNullOrEmpty(newAutor) && autors.Any(g => g.Name == newAutor))
            {
                book.Autor = newAutor;
                Info.SuccedKey("Autor was changed");
            }
        }
        [MenuAction("Publish Code", 4)]
        public void ChangePublishCode(Book book)
        {
            Console.Clear();
            Info.Inform($"If you do`t want to change Publish Code press ( Enter )");
            Info.Inform($"Book Publish Code is: {book.PublishCode}");
            try
            {
                var newCode = int.Parse(Console.ReadLine());

                book.PublishCode = newCode;
                Info.SuccedKey("Publish Code was changed");
            
            }
            catch 
            {
                
            }
        }
        [MenuAction("City", 5)]
        public void ChangeCity(Book book)
        {
            Console.Clear();
            Info.Inform($"If you do`t want to change City press ( Enter )");
            Info.Inform($"Book City is: {book.City??="***[ No City ]***"}");

            var newCity = Console.ReadLine();

            if (!string.IsNullOrEmpty(newCity))
            {
                book.City = newCity;
                Info.SuccedKey("City was changed");
            }
        }
        [MenuAction("Country", 6)]
        public void ChangeCountry(Book book)
        {
            Console.Clear();
            Info.Inform($"If you do`t want to change Country press ( Enter )");
            Info.Inform($"Book Country is: {book.Country ??= "***[ No City ]***"}");

            var newCountry = Console.ReadLine();

            if (!string.IsNullOrEmpty(newCountry))
            {
                book.Country = newCountry;
                Info.SuccedKey("Country was changed");
            }
        }
        [MenuAction("Year", 7)]
        public void ChangeYear(Book book)
        {
            Console.Clear();
            Info.Inform($"If you do`t want to change Year press ( Enter )");
            Info.Inform($"Book Year is: {book.Year}");

            try
            {
                book.Year = DateTime.Parse(Console.ReadLine());
                Info.SuccedKey("Year was changed");

            }
            catch { }
        }

        [MenuAction("Borrow", 8)]
        public void ChangeReturnDays(Book book)
        {
            Console.Clear();
            Info.Inform("For how long ( Days ) you allow to borrow this book? ( Enter for 30 )");
            Info.Inform($"Book Return Days is: {book.ReturnDays}");


            if (int.TryParse(Console.ReadLine(), out int days))
                book.ReturnDays = days;
            else
                book.ReturnDays = 30;
        }
    }
}