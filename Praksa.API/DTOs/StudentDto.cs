namespace Praksa.API.DTOs
{
    public class StudentDto
    {
        public int Id { get; set; }
        public string Ime { get; set; } = string.Empty;
        public string Prezime { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string IndexBroj { get; set; } = string.Empty;
        public int Godina { get; set; }
        public int? ProjectId { get; set; }
        public string? ProjectNaziv { get; set; }
    }

    public class StudentCreateDto
    {
        public string Ime { get; set; } = string.Empty;
        public string Prezime { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string IndexBroj { get; set; } = string.Empty;
        public int Godina { get; set; }
    }
}