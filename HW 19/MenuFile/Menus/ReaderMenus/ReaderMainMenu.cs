namespace Library
{
    internal class ReaderMainMenu
    {
        [MenuSubmenu("Information", 1, "Here you can take books and get information")]
        public ReaderInformationMenu InformationMenu { get; set; }
        
        [MenuSubmenu("My History", 2, "Information about books you took")]
        public ReaderHistoryMenu HistoryMenu { get; set; }
    }
}