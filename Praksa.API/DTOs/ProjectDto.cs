using Praksa.API.Model;

namespace Praksa.API.DTOs
{
    public class ProjectDto
    {
        public int Id { get; set; }
        public string Naziv { get; set; } = string.Empty;
        public string Opis { get; set; } = string.Empty;
        public int MaxStudenata { get; set; }
        public ProjectStatus Status { get; set; }
        public int MentorId { get; set; }
        public string? MentorImePrezime { get; set; }
        public int BrojPrijavljenih { get; set; }
    }

    public class ProjectCreateDto
    {
        public string Naziv { get; set; } = string.Empty;
        public string Opis { get; set; } = string.Empty;
        public int MaxStudenata { get; set; }
        public int MentorId { get; set; }
    }
}