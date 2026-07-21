// Controllers/StudentsController.cs
using Microsoft.AspNetCore.Mvc;
using Praksa.API.DTOs;
using Praksa.API.Helpers;
using Praksa.API.Interfaces;

namespace Praksa.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StudentsController : ControllerBase
    {
        private readonly IStudentService _studentService;

        public StudentsController(IStudentService studentService)
        {
            _studentService = studentService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var students = await _studentService.GetAllAsync();
            return Ok(ApiResponse<IEnumerable<StudentDto>>.Ok(students));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var student = await _studentService.GetByIdAsync(id);
            if (student is null) return NotFound(ApiResponse<StudentDto>.Fail("Student nije pronađen."));
            return Ok(ApiResponse<StudentDto>.Ok(student));
        }

        [HttpPost]
        public async Task<IActionResult> Create(StudentCreateDto dto)
        {
            var student = await _studentService.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = student.Id }, ApiResponse<StudentDto>.Ok(student));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var success = await _studentService.DeleteAsync(id);
            if (!success) return NotFound(ApiResponse<object>.Fail("Student nije pronađen."));
            return NoContent();
        }

        [HttpPost("assign")]
        public async Task<IActionResult> AssignProject(AssignProjectDto dto)
        {
            await _studentService.AssignProjectAsync(dto.StudentId, dto.ProjectId);
            return Ok(ApiResponse<object>.Ok(null, "Student je uspješno dodijeljen projektu."));
        }

        [HttpPost("{id}/unassign")]
        public async Task<IActionResult> UnassignProject(int id)
        {
            var success = await _studentService.UnassignProjectAsync(id);
            if (!success) return NotFound(ApiResponse<object>.Fail("Student nije pronađen."));
            return Ok(ApiResponse<object>.Ok(null, "Studentu je uklonjen projekat."));
        }
    }
}