using Librar.DAL.Interface;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace Librar.DAL.Models
{
    public class Reader : IEntry, IPerson
    {
        [Key]
        public int Id { get; set; }
        [MaxLength(400)]
        public string Login { get; set; } = null!;
        public string Password { get; set; } = null!;
        [EmailAddress]
        public string Mail { get; set; } = null!;
        [MaxLength(400)]
        public string Name { get; set; } = null!;
        public string? LastName { get; set; }
        public string? SecondName { get; set; }
        public DateTime? Birthday { get; set; }
        public string DocumentType { get; set; } = null!;
        public int DocumentNumber { get; set; }

        [ForeignKey(nameof(Id))]
        public virtual ICollection<BorrowedBooks> BorrowedBooks { get; set; } = new List<BorrowedBooks>();


        [ForeignKey(nameof(DocumentType))]
        public virtual DocumentType DocumentTypeNavigation { get; set; } = null!;
        
        [MaxLength(400)]
        public string LibrarianLog { get; set; } = null!;
        public virtual Librarian Librarian { get; set; } = null!;
    }
}
