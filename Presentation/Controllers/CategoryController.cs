using AuthService.Application.Services.Interfaces;
using AuthService.Domain.Entities.Concretes;
using Microsoft.AspNetCore.Mvc;

namespace AuthService.Presentation.Controllers
{
    [ApiController, Route("api/[controller]")]
    public class CategoryController(IService<Category, int> service) : ControllerBase
    {
        private readonly IService<Category, int> _service = service;

        [HttpGet]
        public async Task<IActionResult> GetAll(int pageNumber = 1, int pageSize = 10)
        {
            var categories = await _service.GetAll(pageNumber, pageSize);
            return Ok(categories);
        }

        [HttpGet("id/{id}/permissions")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _service.GetById(id);
            if (result is null)
            {
                return NotFound("Category not found");
            }
            return Ok(result);
        }
    }
}
