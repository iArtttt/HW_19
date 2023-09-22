namespace Library.API.Dtos
{
    public record UserRegistrationDto(string Login, string Password, string Email, string Name, string DocumentType, int DocumentNumber)
    {
        public string? LastName { get; set; }
        public string? SecondName { get; set; }
        public DateTime? Birthday { get; set; }

    };
}
