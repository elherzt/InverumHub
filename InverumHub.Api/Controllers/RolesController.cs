using InverumHub.Core.DTOs;
using InverumHub.Core.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InverumHub.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "SSOT_ADMIN")]
    public class RolesController : Controller
    {

        private readonly IRoleService _roleService;

        public RolesController(IRoleService roleService)
        {
            _roleService = roleService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var roles = await _roleService.GetAll();
            return Ok(roles);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var role = await _roleService.GetById(id);
            return Ok(role);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateAndUpdateRoleDTO model)
        {
            var role = await _roleService.Create(model);
            return CreatedAtAction(nameof(GetById), new { id = role.Id }, role);
        }

        [HttpPut]
        public async Task<IActionResult> Update(CreateAndUpdateRoleDTO model)
        {
            var role = await _roleService.Update(model);
            return Ok(role);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _roleService.Delete(id);
            return NoContent();
        }
    }
}
