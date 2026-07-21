// Interfaces/IStudentService.cs
using Praksa.API.DTOs;

namespace Praksa.API.Interfaces
{
    public interface IStudentService
    {
        Task<IEnumerable<StudentDto>> GetAllAsync();
        Task<StudentDto?> GetByIdAsync(int id);
        Task<StudentDto> CreateAsync(StudentCreateDto dto);
        Task<bool> DeleteAsync(int id);
        Task<bool> AssignProjectAsync(int studentId, int projectId);
        Task<bool> UnassignProjectAsync(int studentId);
    }
}