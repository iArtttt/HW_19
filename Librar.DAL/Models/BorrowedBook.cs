using Library.Common.Interface;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Librar.DAL.Models
{
    public class BorrowedBook : IName
    {
        [Key] 
        public int ID { get; set; }

        [MaxLength(400)]
        public string Name { get; set; } = null!;
        
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
