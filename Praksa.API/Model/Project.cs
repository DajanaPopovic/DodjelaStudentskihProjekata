namespace Praksa.API.Model
{
    public enum ProjectStatus
    {
        Otvoren,
        Popunjen,
        Zatvoren
    }

    public class Project
    {
        public int Id { get; set; }
        public string Naziv { get; set; } = string.Empty;
        public string Opis { get; set; } = string.Empty;
        public int MaxStudenata { get; set; }
        public ProjectStatus Status { get; set; } = ProjectStatus.Otvoren;

        public int MentorId { get; set; }
        public Mentor? Mentor { get; set; }

        public ICollection<Student> Studenti { get; set; } = new List<Student>();
    }
}