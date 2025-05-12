using AuthService.Application.Decoders.JWT;
using AuthService.Application.Services.Interfaces;
using AuthService.Domain.DTOs.Categories;
using AuthService.Domain.Entities.Concretes;
using Microsoft.AspNetCore.Mvc;

namespace AuthService.Presentation.Controllers
{
    [ApiController, Route("api/[controller]")]
    public class CategoryController(
        IService<Category, int> service
    ) : ControllerBase
    {
        private readonly IService<Category, int> _service = service;

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            var categories = await _service.GetAll(pageNumber, pageSize, null);
            return Ok(categories);
        }

        [HttpGet("id/{id}/permissions")]
        public async Task<IActionResult> GetById([FromQuery] int id)
        {
            var result = await _service.GetById(id, Guid.Empty);
            if (result is null)
            {
                return NotFound("Category not found");
            }
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateCategoryDTO category)
        {
            var entity = new Category
            {
                Name = category.Name
            };
            var result = await _service.Create(entity, null);
            if (result is null)
            {
                return BadRequest("Category can not be created");
            }
            return Ok(result);
        }
    }
}
