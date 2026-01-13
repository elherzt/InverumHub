using InverumHub.Core.DTOs;
using InverumHub.Core.Services;
using Microsoft.AspNetCore.Mvc;

namespace InverumHub.Api.Controllers
{
    [ApiController]
    [Route("api/users/{userId}/applications/{appId}/roles")]
    public class UserApplicationRolesController : Controller
    {

        private readonly IUserService _userService;

        public UserApplicationRolesController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost]
        public async Task<IActionResult> AddRoleToUserApplication(Guid userId, int appId, AssignRoleDTO role)
        {
            var updated_user = await _userService.AddRoleApplication(userId, role.RoleId, appId);
            return Ok(updated_user);
        }

        [HttpDelete("{roleId}")]
        public async Task<IActionResult> RemoveRoleFromUserApplication(Guid userId, int appId, int roleId)
        {
            await _userService.RemoveRoleApplication(userId, roleId, appId);
            return NoContent();
        }
    }
}
