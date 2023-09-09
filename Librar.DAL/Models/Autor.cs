using Librar.DAL.Interface;
using System.ComponentModel.DataAnnotations;

namespace Librar.DAL.Models
{
    public class Autor : IPerson
    {
        [Key]
        public int ID { get; set; }
        
        [MaxLength(400)]
        public string Name { get; set; } = null!;
        public string? LastName { get; set; }
        public string? SecondName { get; set; }
        public DateTime? Birthday { get; set; }
        public virtual ICollection<Book> Books { get; set; } = new List<Book>();

        public override string ToString()
        {
            return $"{Name} {LastName} {SecondName} {Birthday}";
        }
    }
}