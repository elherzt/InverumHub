using InverumHub.Core.DTOs;
using InverumHub.Core.Services;
using Microsoft.AspNetCore.Mvc;

namespace InverumHub.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : Controller
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }


        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok("to do...");
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            return Ok("to do...");
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateUserDTO model)
        {
            var user = await _userService.CreateUser(model);
            return CreatedAtAction(nameof(Get), new { id = user.Uid }, user);
        }
    }
}
