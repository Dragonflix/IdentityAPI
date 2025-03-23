using IdentityBLL.Interfaces;
using IdentityBLL.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace IdentityAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService projectService)
        {
            _userService = projectService;
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var projects = await _userService.GetAllAsync();
            return Ok(projects);
        }

        [Authorize]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var project = await _userService.GetByIdAsync(id);
            if (project == null)
            {
                return NotFound();
            }
            return Ok(project);
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register(UserModel model)
        {
            await _userService.AddAsync(model);
            return CreatedAtAction(nameof(GetById), new { id = model.UserId }, model);
        }

        [HttpPost("Authorize")]
        public async Task<IActionResult> Authorize(string email, string password)
        {
            var token = await _userService.AuthorizeAsync(email, password);
            return Ok(token);
        }

        [Authorize]
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, UserModel model)
        {
            if (id != model.UserId)
            {
                return BadRequest();
            }
            await _userService.UpdateAsync(model);
            return NoContent();
        }

        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _userService.DeleteAsync(id);
            return NoContent();
        }
    }
}
