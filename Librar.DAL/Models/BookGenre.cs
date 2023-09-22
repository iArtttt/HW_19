using System.ComponentModel.DataAnnotations;

namespace Librar.DAL.Models
{
    public class BookGenre
    {
        [Key]
        [MaxLength(400)]
        public string Genre { get; set; } = null!;
        public virtual ICollection<Book> Books { get; set;} = new List<Book>();
    }
}
