using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Librar.DAL.Models
{
    public class PublishingCodeType
    {
        public int Id { get; set; }

        [Key]
        public int CodeType { get; set; }

        public virtual ICollection<Book> Books { get; set; } = new List<Book>();
    }
}