namespace Praksa.API.Model
{
    public class Mentor
    {
        public int Id { get; set; }
        public string Ime { get; set; } = string.Empty;
        public string Prezime { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;

        public ICollection<Project> Projekti { get; set; } = new List<Project>();
    }
}