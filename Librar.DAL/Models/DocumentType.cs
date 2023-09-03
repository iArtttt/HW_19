using System.ComponentModel.DataAnnotations;

namespace Librar.DAL.Models
{
    public class DocumentType
    {
        [Key]
        public int Id { get; set; }
        [Key]
        [Required]
        public string Type { get; set; } = null!;
        public ICollection<Reader> DocumentTypes { get; set; } = new List<Reader>();
    }
}