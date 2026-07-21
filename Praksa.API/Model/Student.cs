// Models/Student.cs
namespace Praksa.API.Model
{
    public class Student
    {
        public int Id { get; set; }
        public string Ime { get; set; } = string.Empty;
        public string Prezime { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string IndexBroj { get; set; } = string.Empty;
        public int Godina { get; set; }

        public int? ProjectId { get; set; }
        public Project? Project { get; set; }
    }
}