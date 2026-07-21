// Controllers/ProjectsController.cs
using Microsoft.AspNetCore.Mvc;
using Praksa.API.DTOs;
using Praksa.API.Helpers;
using Praksa.API.Interfaces;

namespace Praksa.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProjectsController : ControllerBase
    {
        private readonly IProjectService _projectService;

        public ProjectsController(IProjectService projectService)
        {
            _projectService = projectService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var projects = await _projectService.GetAllAsync();
            return Ok(ApiResponse<IEnumerable<ProjectDto>>.Ok(projects));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var project = await _projectService.GetByIdAsync(id);
            if (project is null) return NotFound(ApiResponse<ProjectDto>.Fail("Projekat nije pronađen."));
            return Ok(ApiResponse<ProjectDto>.Ok(project));
        }

        [HttpPost]
        public async Task<IActionResult> Create(ProjectCreateDto dto)
        {
            var project = await _projectService.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = project.Id }, ApiResponse<ProjectDto>.Ok(project));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var success = await _projectService.DeleteAsync(id);
            if (!success) return NotFound(ApiResponse<object>.Fail("Projekat nije pronađen."));
            return NoContent();
        }
    }
}