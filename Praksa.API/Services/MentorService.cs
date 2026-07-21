// Services/MentorService.cs
using Microsoft.EntityFrameworkCore;
using Praksa.API.Data;
using Praksa.API.DTOs;
using Praksa.API.Helpers;
using Praksa.API.Interfaces;
using Praksa.API.Model;

namespace Praksa.API.Services
{
    public class MentorService : IMentorService
    {
        private readonly ApplicationDbContext _context;

        public MentorService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<MentorDto>> GetAllAsync()
        {
            return await _context.Mentors.Select(m => MapToDto(m)).ToListAsync();
        }

        public async Task<MentorDto?> GetByIdAsync(int id)
        {
            var mentor = await _context.Mentors.FindAsync(id);
            return mentor is null ? null : MapToDto(mentor);
        }

        public async Task<MentorDto> CreateAsync(MentorCreateDto dto)
        {
            var exists = await _context.Mentors.AnyAsync(m => m.Email == dto.Email);
            if (exists)
                throw new BusinessException("Mentor sa datim emailom već postoji.");

            var mentor = new Mentor
            {
                Ime = dto.Ime,
                Prezime = dto.Prezime,
                Email = dto.Email
            };

            _context.Mentors.Add(mentor);
            await _context.SaveChangesAsync();

            return MapToDto(mentor);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var mentor = await _context.Mentors.FindAsync(id);
            if (mentor is null) return false;

            _context.Mentors.Remove(mentor);
            await _context.SaveChangesAsync();
            return true;
        }

        private static MentorDto MapToDto(Mentor m) => new()
        {
            Id = m.Id,
            Ime = m.Ime,
            Prezime = m.Prezime,
            Email = m.Email
        };
    }
}