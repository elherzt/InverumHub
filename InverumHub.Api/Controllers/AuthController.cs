using InverumHub.Api.Common.JWT;
using InverumHub.Core.Common;
using InverumHub.Core.DTOs;
using InverumHub.Core.Services;
using Microsoft.AspNetCore.Mvc;

namespace InverumHub.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : Controller
    {
        
        private readonly IAuthService _authService;
        private readonly IJWTGenerator _jwtGenerator;

        public AuthController(IAuthService authService, IJWTGenerator jWTGenerator)
        {
            _authService = authService;
            _jwtGenerator = jWTGenerator;
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginDTO model)
        {
            GlobalSessionModel sessionModel = await _authService.login(model);
            string token = _jwtGenerator.GenerateToken(sessionModel);
            return Ok(new { Token = token });

        }
    }
}
