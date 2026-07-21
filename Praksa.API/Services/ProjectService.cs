// Services/ProjectService.cs
using Microsoft.EntityFrameworkCore;
using Praksa.API.Data;
using Praksa.API.DTOs;
using Praksa.API.Helpers;
using Praksa.API.Interfaces;
using Praksa.API.Model;

namespace Praksa.API.Services
{
    public class ProjectService : IProjectService
    {
        private readonly ApplicationDbContext _context;

        public ProjectService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ProjectDto>> GetAllAsync()
        {
            return await _context.Projects
                .Include(p => p.Mentor)
                .Include(p => p.Studenti)
                .Select(p => MapToDto(p))
                .ToListAsync();
        }

        public async Task<ProjectDto?> GetByIdAsync(int id)
        {
            var project = await _context.Projects
                .Include(p => p.Mentor)
                .Include(p => p.Studenti)
                .FirstOrDefaultAsync(p => p.Id == id);

            return project is null ? null : MapToDto(project);
        }

        public async Task<ProjectDto> CreateAsync(ProjectCreateDto dto)
        {
            var mentorExists = await _context.Mentors.AnyAsync(m => m.Id == dto.MentorId);
            if (!mentorExists)
                throw new NotFoundException("Mentor nije pronađen.");

            var project = new Project
            {
                Naziv = dto.Naziv,
                Opis = dto.Opis,
                MaxStudenata = dto.MaxStudenata,
                MentorId = dto.MentorId,
                Status = ProjectStatus.Otvoren
            };

            _context.Projects.Add(project);
            await _context.SaveChangesAsync();

            return await GetByIdAsync(project.Id) ?? MapToDto(project);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var project = await _context.Projects.FindAsync(id);
            if (project is null) return false;

            _context.Projects.Remove(project);
            await _context.SaveChangesAsync();
            return true;
        }

        private static ProjectDto MapToDto(Project p) => new()
        {
            Id = p.Id,
            Naziv = p.Naziv,
            Opis = p.Opis,
            MaxStudenata = p.MaxStudenata,
            Status = p.Status,
            MentorId = p.MentorId,
            MentorImePrezime = p.Mentor is null ? null : $"{p.Mentor.Ime} {p.Mentor.Prezime}",
            BrojPrijavljenih = p.Studenti?.Count ?? 0
        };
    }
}