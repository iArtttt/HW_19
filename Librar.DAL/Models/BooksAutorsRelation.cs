using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Librar.DAL.Models
{
    public class BooksAutorsRelation
    {
        [Key]
        public int Id { get; set; }

        //[System.ComponentModel.DataAnnotations.Schema.]
        //public int AutorId { get; set; }

        //[ForeignKey(nameof(Book))]
        //public int BookId { get; set; }
        [ForeignKey(nameof(Autor))]
        public Autor AutorId { get; set; } = null!;

        [ForeignKey(nameof(Book))]
        public Book BookId { get; set; } = null!;
    }
}