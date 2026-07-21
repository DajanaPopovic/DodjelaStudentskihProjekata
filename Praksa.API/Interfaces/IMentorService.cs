// Interfaces/IMentorService.cs
using Praksa.API.DTOs;

namespace Praksa.API.Interfaces
{
    public interface IMentorService
    {
        Task<IEnumerable<MentorDto>> GetAllAsync();
        Task<MentorDto?> GetByIdAsync(int id);
        Task<MentorDto> CreateAsync(MentorCreateDto dto);
        Task<bool> DeleteAsync(int id);
    }
}