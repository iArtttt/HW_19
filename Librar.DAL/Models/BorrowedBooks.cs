using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Librar.DAL.Models
{
    public class BorrowedBooks
    {
        [Key] 
        public int Id { get; set; }
        
        [MaxLength(400)]
        public int BookID { get; set; }
        
        [MaxLength(400)]
        public int ReaderID { get; set; }

        public DateTime Taked { get; set; }
        public DateTime ReturneTo { get; set; }

        public bool IsReturned { get; set; }
        public bool WasOverdue { get; set; }
        public string StoryTook { get; set; } = null!;
        public string? StoryBack { get; set; }
        
        [ForeignKey(nameof(BookID))]
        public virtual Book Book { get; set;} = null!;

        [ForeignKey(nameof(ReaderID))]
        public virtual Reader Reader { get; set; } = null!;
    }
}
