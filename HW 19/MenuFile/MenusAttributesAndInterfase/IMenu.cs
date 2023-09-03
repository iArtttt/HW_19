namespace Library
{
    public interface IMenu : IMenuItem
    {
        IEnumerable<IMenuItem> Items { get; }
    }
}