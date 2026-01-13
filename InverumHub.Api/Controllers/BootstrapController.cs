using InverumHub.Core.DTOs;
using InverumHub.Core.Entities;
using InverumHub.Core.Services;
using Microsoft.AspNetCore.Mvc;

namespace InverumHub.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BootstrapController : Controller
    {
        private readonly IUserService _userService;

        public BootstrapController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("InitializeAdmin")]
        public async Task<IActionResult> InitializeAdmin(CreateUserDTO model)
        {
            var adminUser = await _userService.InitializeAdminUser(model);
            return Ok(adminUser);
        }
    }
}
