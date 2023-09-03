using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Librar.DAL.Models
{
    public class Book
    {
        [Key]
        public int ID { get; set; }
        [Key]
        [MaxLength(400)]
        public string Name { get; set; } = null!;
        [MaxLength(400)]
        public string Genre { get; set; } = null!;
        public string Autor { get; set; } = null!;
        public DateTime? Year { get; set; }
        public string? Country { get; set; }
        public string? City { get; set; }
        public int PublishCode { get; set; }
        public string? PublishCodeType { get; set; }
        public Autor AutorNavigation { get; set;} = null!;
        public ICollection<BooksAutorsRelation> BooksAutorsRelations { get; set; } = new List<BooksAutorsRelation>();
        public PublishingCodeType PublishingCodeTypeNavigation { get; set; } = null!;
    }
}
