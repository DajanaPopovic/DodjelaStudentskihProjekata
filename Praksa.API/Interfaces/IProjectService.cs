// Interfaces/IProjectService.cs
using Praksa.API.DTOs;

namespace Praksa.API.Interfaces
{
    public interface IProjectService
    {
        Task<IEnumerable<ProjectDto>> GetAllAsync();
        Task<ProjectDto?> GetByIdAsync(int id);
        Task<ProjectDto> CreateAsync(ProjectCreateDto dto);
        Task<bool> DeleteAsync(int id);
    }
}