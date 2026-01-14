using InverumHub.Api.Common;
using InverumHub.Core.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace InverumHub.Api.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/[controller]")]
    public class PermissionsController : Controller
    {
        private readonly IPermissionsService _permissionsService;

        public PermissionsController(IPermissionsService permissionsService)
        {
            _permissionsService = permissionsService;
        }

        [HttpGet]
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
