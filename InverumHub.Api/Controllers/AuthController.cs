using InverumHub.Api.Common;
using InverumHub.Api.Common.JWT;
using InverumHub.Core.Common;
using InverumHub.Core.DTOs;
using InverumHub.Core.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace InverumHub.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : Controller
    {

        private readonly IAuthService _authService;
        private readonly IJWTGenerator _jwtGenerator;
        private readonly IPermissionsService _permissionsService;

        public AuthController(IAuthService authService, IJWTGenerator jWTGenerator, IPermissionsService permissionsService)
        {
            _authService = authService;
            _jwtGenerator = jWTGenerator;
            _permissionsService = permissionsService;
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginDTO model)
        {
            GlobalSessionModel sessionModel = await _authService.login(model);
            string token = _jwtGenerator.GenerateToken(sessionModel);
            return Ok(new { Token = token });

        }

        [Authorize]
        [HttpPut("me/ChangePassword")]
        public async Task<IActionResult> ChangeOwnPassword(ChangeOwnPasswordDTO model)
        {
            var sessionModel = User.ToGlobalSession();
            await _authService.ChangePassword(sessionModel.UserId, model);
            return NoContent();
        }

        [Authorize]
        [HttpGet("me/Permissions")]
        public async Task<IActionResult> GetPermissions(
            [FromQuery, Required] string roleName,
            [FromQuery, Required] string applicationName)
        {
            var sessionModel = User.ToGlobalSession();
            var permissios = await _permissionsService.ChekPermissions(sessionModel, roleName, applicationName);
            return Ok(permissios);
        }
    }
}
