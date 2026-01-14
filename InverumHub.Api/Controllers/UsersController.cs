using InverumHub.Core.DTOs;
using InverumHub.Core.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InverumHub.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "SSOT_ADMIN")]
    public class UsersController : Controller
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }


        [HttpGet]
        public async Task<IActionResult> Get(
            [FromQuery] bool? isActive)
        {
            if (isActive == null) {
                isActive = true;
            }
            var users = await _userService.GetAll(isActive.Value);
            return Ok(users);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var user = await _userService.GetById(id);
            return Ok(user);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateUserDTO model)
        {
            var user = await _userService.CreateUser(model);
            return CreatedAtAction(nameof(Get), new { id = user.Uid }, user);
        }

        [HttpPut]
        public async Task<IActionResult> Update(UpdateUserDTO model)
        {
            var user = await _userService.UpdateUser(model);
            return Ok(user);
        }

        [HttpPut("{id}/ChangePassword")]
        public async Task<IActionResult> ChangePassword(Guid id, ChangePasswordDTO model)
        {
           
            var user = await _userService.ChangePassword(id, model);
            return Ok(user);
        }

        // DELETE api/users/{id} - Optional: Implement user deletion if needed, since user can be disabled instead, not implemented here.

    }
}
