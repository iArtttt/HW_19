using Library.Common.Interface;
using System.ComponentModel.DataAnnotations;

namespace Librar.DAL.Models
{
    public class Librarian : IUser
    {
        [Key]
        public int ID { get; set; }

        [MaxLength(400)]
        public string Login { get; set; } = null!;
        public string Password { get; set; } = null!;
        [EmailAddress]
        public string Mail { get; set; } = null!;
        public virtual ICollection<Reader> Readers { get; set; } = new List<Reader>();
    }
}
