namespace Library.API.Dtos
{
    public record class ChangeAutorDto()
    {
        public string? Name { get; set; }
        public string? LastName { get; set; }
        public string? SecondName { get; set; }
        public DateTime? Birthday { get; set; }
    }
}
