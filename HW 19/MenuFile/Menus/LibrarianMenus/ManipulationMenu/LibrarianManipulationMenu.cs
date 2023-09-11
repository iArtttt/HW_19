namespace Library
{
    internal class LibrarianManipulationMenu
    {
        [MenuSubmenu("Books", 1, "Add or change Books")]
        public BooksChangingMenu BooksChangingMenu { get; set; }

        [MenuSubmenu("Autors", 2, "Add or change Autor")]
        public AutorChangingMenu AutorChangingMenu { get; set; }

        [MenuSubmenu("Readers", 3, "Add, change, remove Readers")]
        public LibrarianReaderManipulationMenu ReaderMenu { get; set; }
    }
}