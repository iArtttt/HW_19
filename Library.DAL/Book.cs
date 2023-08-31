using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Library.DAL;

public partial class Book
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;
    [MaxLength(400)]
    public string? Genre { get; set; }

    public int Autors { get; set; }

    public int PublishingCode { get; set; }

    public int PublishingCodeType { get; set; }

    public DateTime Year { get; set; }

    public string? CountryPublish { get; set; }

    public string? SityPublish { get; set; }

    public virtual Autor AutorsNavigation { get; set; } = null!;

    public virtual ICollection<BooksAutorsRelation> BooksAutorsRelations { get; set; } = new List<BooksAutorsRelation>();

    public virtual PublishingCodeType PublishingCodeTypeNavigation { get; set; } = null!;
}
