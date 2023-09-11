namespace Library
{
    internal class LibrarianMainMenu
    {
        [MenuSubmenu("Search", 1, "Find books and autors")]
        public LibrarianSearchMenu SearchMenu { get; set; }

        [MenuSubmenu("Manipulations", 2, "Autors, Books and Readers manipulations")]
        public LibrarianManipulationMenu ManipulationMenu { get; set; }


        [MenuSubmenu("Information", 3, "Chek story and actual information about Readers")]
        public LibrarianInformationMenu InformationMenu { get; set; }

    }
}