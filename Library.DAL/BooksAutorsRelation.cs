﻿using System;
using System.Collections.Generic;

namespace Library.DAL;

public partial class BooksAutorsRelation
{
    public int Id { get; set; }

    public int AutorId { get; set; }

    public int BookId { get; set; }

    public virtual Autor Autor { get; set; } = null!;

    public virtual Book Book { get; set; } = null!;
}
