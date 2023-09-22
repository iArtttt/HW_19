using Library.Common.Interface;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Librar.DAL.Models
{
    public class Book : IName
    {
        [Key]
        public int ID { get; set; }
        
        [MaxLength(400)]
        public string Name { get; set; } = null!;
        
        [MaxLength(400)]
        public string Genre { get; set; } = null!;

        [MaxLength(400)]
        public string Autor { get; set; } = null!;
        public int Count { get; set; }
        public DateTime? Year { get; set; }
        public string? Country { get; set; }
        public string? City { get; set; }
        public int PublishCode { get; set; }
        public int PublishCodeType { get; set; }
        public int ReturnDays { get; set; } = 30;

        [ForeignKey(nameof(Autor))]
        public virtual Autor AutorNavigation { get; set;} = null!;
        
        [ForeignKey(nameof(PublishCodeType))]
        public virtual PublishingCodeType PublishingCodeTypeNavigation { get; set; } = null!;

        [ForeignKey(nameof(Genre))]
        public virtual BookGenre GenreId { get; set; } = null!;
    }
}
