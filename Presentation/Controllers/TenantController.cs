using AuthService.Application.Services.Interfaces;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace AuthService.Presentation.Controllers
{
    [ApiController, Route("api/[controller]")]
    public class TenantController(
        ITenantService service
    ) : ControllerBase
    {
        private readonly ITenantService _service = service;

        [HttpPost("initialize/tenant/{id}")]
        public async Task<IActionResult> InitializeTenant(Guid id)
        {
            var result = await _service.Initialize(id);
            return result ? Ok() : BadRequest();
        }
    }
}
