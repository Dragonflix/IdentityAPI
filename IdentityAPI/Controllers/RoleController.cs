using IdentityBLL.Interfaces;
using IdentityBLL.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace IdentityAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly IRoleService _roleService;

        public RoleController(IRoleService projectService)
        {
            _roleService = projectService;
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var projects = await _roleService.GetAllAsync();
            return Ok(projects);
        }

        [Authorize]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var project = await _roleService.GetByIdAsync(id);
            if (project == null)
            {
                return NotFound();
            }
            return Ok(project);
        }

        //[Authorize]
        [HttpPost]
        public async Task<IActionResult> Add(RoleModel model)
        {
            await _roleService.AddAsync(model);
            return CreatedAtAction(nameof(GetById), new { id = model.RoleId }, model);
        }

        [Authorize]
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, RoleModel model)
        {
            if (id != model.RoleId)
            {
                return BadRequest();
            }
            await _roleService.UpdateAsync(model);
            return NoContent();
        }

        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _roleService.DeleteAsync(id);
            return NoContent();
        }
    }
}
