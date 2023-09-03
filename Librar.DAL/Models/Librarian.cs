using Librar.DAL.Interface;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Librar.DAL.Models
{
    public class Librarian : IEntry
    {
        [Key]
        public int ID { get; set; }

        [Key]
        [MaxLength(400)]
        public string Login { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string Mail { get; set; } = null!;
        public ICollection<Reader> Readers { get; set; } = new List<Reader>();
    }
}
