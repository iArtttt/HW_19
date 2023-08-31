using System;
using System.Collections.Generic;

namespace Library.DAL;

public partial class Librarian
{
    public int Id { get; set; }

    public string Login { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string Mail { get; set; } = null!;
}
