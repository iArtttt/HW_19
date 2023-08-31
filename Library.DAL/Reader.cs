using System;
using System.Collections.Generic;

namespace Library.DAL;

public partial class Reader
{
    public int Id { get; set; }

    public string Login { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string Mail { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string? LastName { get; set; }

    public string? DocumentType { get; set; }

    public int DocumentNumber { get; set; }

    public virtual DocumentType? DocumentTypeNavigation { get; set; }
}
