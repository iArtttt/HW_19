namespace Library
{
    public interface IMenuItem
    {
        string? Title { get; }
        string? Description { get; }
        void Process();
    }
}