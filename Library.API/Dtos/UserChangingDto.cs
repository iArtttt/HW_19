namespace Library.API.Dtos
{
    public record UserChangingDto()
    {
        public string? Login{ get; set; }
        public string? Password{ get; set; }
        public string? Email{ get; set; }
        public string? Name { get; set; }
        public string? DocumentType{ get; set; }
        public int? DocumentNumber { get; set; }
        public string? LastName { get; set; }
        public string? SecondName { get; set; }
        public DateTime? Birthday { get; set; }

    };
}
