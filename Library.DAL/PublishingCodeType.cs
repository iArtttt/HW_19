using System;
using System.Collections.Generic;

namespace Library.DAL;

public partial class PublishingCodeType
{
    public int Id { get; set; }

    public int CodeType { get; set; }

    public virtual ICollection<Book> Books { get; set; } = new List<Book>();
}
