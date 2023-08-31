namespace Library
{
    internal class MenuItem : IMenuItem
    {
        private readonly Action? _process;
        public string? Title { get; }

        public string? Description { get; } = string.Empty;


        public MenuItem(string? title, Action? process = null, string? description = null)
        {
            _process = process ?? new Action(() => { });
            Title = title;
            Description = description;
        }

        public virtual void Process() => _process();

    }
}