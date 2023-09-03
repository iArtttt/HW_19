namespace Librar.DAL.Models
{
    public class PublishingCodeType
    {
        public int Id { get; set; }

        public int CodeType { get; set; }

        public ICollection<Book> Books { get; set; } = new List<Book>();
    }
}