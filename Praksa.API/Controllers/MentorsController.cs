// Controllers/MentorsController.cs
using Microsoft.AspNetCore.Mvc;
using Praksa.API.DTOs;
using Praksa.API.Helpers;
using Praksa.API.Interfaces;

namespace Praksa.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MentorsController : ControllerBase
    {
        private readonly IMentorService _mentorService;

        public MentorsController(IMentorService mentorService)
        {
            _mentorService = mentorService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var mentors = await _mentorService.GetAllAsync();
            return Ok(ApiResponse<IEnumerable<MentorDto>>.Ok(mentors));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var mentor = await _mentorService.GetByIdAsync(id);
            if (mentor is null) return NotFound(ApiResponse<MentorDto>.Fail("Mentor nije pronađen."));
            return Ok(ApiResponse<MentorDto>.Ok(mentor));
        }

        [HttpPost]
        public async Task<IActionResult> Create(MentorCreateDto dto)
        {
            var mentor = await _mentorService.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = mentor.Id }, ApiResponse<MentorDto>.Ok(mentor));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var success = await _mentorService.DeleteAsync(id);
            if (!success) return NotFound(ApiResponse<object>.Fail("Mentor nije pronađen."));
            return NoContent();
        }
    }
}