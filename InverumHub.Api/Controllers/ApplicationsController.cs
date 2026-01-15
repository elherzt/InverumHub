using InverumHub.Core.DTOs;
using InverumHub.Core.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InverumHub.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "SSOT_ADMIN")]
    public class ApplicationsController : Controller
    {
        private readonly IApplicationService _applicationService;

        public ApplicationsController(IApplicationService applicationService)
        {
            _applicationService = applicationService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var apps = await _applicationService.GetAll();
            return Ok(apps);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var app = await _applicationService.GetById(id);
            return Ok(app);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateAndUpdateApplicationDTO model)
        {
            var app = await _applicationService.Create(model);
            return CreatedAtAction(nameof(GetById), new { id = app.Id }, app);
        }

        [HttpPut]
        public async Task<IActionResult> Update(CreateAndUpdateApplicationDTO model)
        {
            var app = await _applicationService.Update(model);
            return Ok(app);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _applicationService.Delete(id);
            return NoContent();
        }
    }
}
