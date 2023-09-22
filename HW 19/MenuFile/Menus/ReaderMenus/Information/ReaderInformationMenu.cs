namespace Library
{
    internal class ReaderInformationMenu
    {
        [MenuSubmenu("Search", 1, "Find book book you want to take")]
        public ReaderSearchMenu SearchMenu { get; set; }
    }
}
