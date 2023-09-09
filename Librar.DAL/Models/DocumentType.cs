using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Librar.DAL.Models
{
    public class DocumentType
    {
        public int Id { get; set; }
        [Key]
        [MaxLength(255)]
        public string Type { get; set; } = null!;
        public virtual ICollection<Reader> Readers { get; set; } = new List<Reader>();
    }
}