using InverumHub.Core.DTOs;
using InverumHub.Core.Repositories;
using InverumHub.Core.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InverumHub.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "SSOT_ADMIN")]
    public class PermissionsController : Controller
    {
        private readonly IPermissionsService _permissionsService;

        public PermissionsController(IPermissionsService permissionsService)
        {
            _permissionsService = permissionsService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var permissions = await _permissionsService.GetAll();
            return Ok(permissions);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var permission = await _permissionsService.GetById(id);
            return Ok(permission);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreatePermissionDTO model)
        {
            var permission = await _permissionsService.Create(model);
            return CreatedAtAction(nameof(GetAll), new { id = permission.Id }, permission);
        }

        [HttpPut]
        public async Task<IActionResult> Update(UpdatePermissionDTO model)
        {
            var permission = await _permissionsService.Update(model);
            return Ok(permission);
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _permissionsService.Delete(id);
            return NoContent();
        }

       

    }
}
