namespace Library.API.Dtos
{
    public record class AddBookDto(string Name, string Genre, string Autor, int PublishCode, int PublishCodeType)
    {
        public string? Country { get; set; }
        public string? City { get; set; }
        public int Count { get; set; } = 1;
        public int ReturnDays { get; set; } = 30;
        public DateTime? Year { get; set; }
    }
}
