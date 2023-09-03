using Librar.DAL.Interface;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace Librar.DAL.Models
{
    public class Reader : IEntry, IPerson
    {
        [Key]
        public int ID { get; set; }

        [Key]
        [MaxLength(400)]
        public string Login { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string Mail { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string? LastName { get; set; }
        public string? SecondName { get; set; }
        public DateTime? Birthday { get; set; }
        public string? DocumentType { get; set; }
        public int DocumentNumber { get; set; }
        public DocumentType? DocumentTypeNavigation { get; set; }
    }
}
