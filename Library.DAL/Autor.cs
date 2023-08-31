using System;
using System.Collections.Generic;

namespace Library.DAL;

public partial class Autor
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string? LastName { get; set; }

    public string? SecondName { get; set; }

    public DateTime Birthday { get; set; }

    public virtual ICollection<Book> Books { get; set; } = new List<Book>();

    public virtual ICollection<BooksAutorsRelation> BooksAutorsRelations { get; set; } = new List<BooksAutorsRelation>();
}
