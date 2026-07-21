// Services/StudentService.cs
using Microsoft.EntityFrameworkCore;
using Praksa.API.Data;
using Praksa.API.DTOs;
using Praksa.API.Helpers;
using Praksa.API.Interfaces;
using Praksa.API.Model;

namespace Praksa.API.Services
{
    public class StudentService : IStudentService
    {
        private readonly ApplicationDbContext _context;

        public StudentService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<StudentDto>> GetAllAsync()
        {
            return await _context.Students
                .Include(s => s.Project)
                .Select(s => MapToDto(s))
                .ToListAsync();
        }

        public async Task<StudentDto?> GetByIdAsync(int id)
        {
            var student = await _context.Students
                .Include(s => s.Project)
                .FirstOrDefaultAsync(s => s.Id == id);

            return student is null ? null : MapToDto(student);
        }

        public async Task<StudentDto> CreateAsync(StudentCreateDto dto)
        {
            var exists = await _context.Students.AnyAsync(s => s.Email == dto.Email || s.IndexBroj == dto.IndexBroj);
            if (exists)
                throw new BusinessException("Student sa datim emailom ili indeksom već postoji.");

            var student = new Student
            {
                Ime = dto.Ime,
                Prezime = dto.Prezime,
                Email = dto.Email,
                IndexBroj = dto.IndexBroj,
                Godina = dto.Godina
            };

            _context.Students.Add(student);
            await _context.SaveChangesAsync();

            return MapToDto(student);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var student = await _context.Students.FindAsync(id);
            if (student is null) return false;

            _context.Students.Remove(student);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> AssignProjectAsync(int studentId, int projectId)
        {
            var student = await _context.Students.FindAsync(studentId)
                ?? throw new NotFoundException("Student nije pronađen.");

            var project = await _context.Projects
                .Include(p => p.Studenti)
                .FirstOrDefaultAsync(p => p.Id == projectId)
                ?? throw new NotFoundException("Projekat nije pronađen.");

            if (project.Studenti.Count >= project.MaxStudenata)
                throw new BusinessException("Projekat je popunjen.");

            student.ProjectId = projectId;

            if (project.Studenti.Count + 1 >= project.MaxStudenata)
                project.Status = ProjectStatus.Popunjen;

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> UnassignProjectAsync(int studentId)
        {
            var student = await _context.Students.FindAsync(studentId);
            if (student is null) return false;

            student.ProjectId = null;
            await _context.SaveChangesAsync();
            return true;
        }

        private static StudentDto MapToDto(Student s) => new()
        {
            Id = s.Id,
            Ime = s.Ime,
            Prezime = s.Prezime,
            Email = s.Email,
            IndexBroj = s.IndexBroj,
            Godina = s.Godina,
            ProjectId = s.ProjectId,
            ProjectNaziv = s.Project?.Naziv
        };
    }
}