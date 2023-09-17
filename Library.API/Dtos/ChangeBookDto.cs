namespace Library.API.Dtos
{
    public record class ChangeBookDto()
    {
        public string? Name { get; set; }
        public string? Genre { get; set; }
        public string? Autor { get; set; }
        public int? PublishCode { get; set; }
        public int? PublishCodeType { get; set; }
        public string? Country { get; set; }
        public string? City { get; set; }
        public int? Count { get; set; }
        public int? ReturnDays { get; set; }
        public DateTime? Year { get; set; }
    }
}
