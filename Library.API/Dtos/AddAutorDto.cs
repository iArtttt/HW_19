namespace Library.API.Dtos
{
    public record class AddAutorDto(string Name)
    {
        public string? LastName { get; set; }
        public string? SecondName { get; set; }
        public DateTime? Birthday { get; set; }
    }
}
