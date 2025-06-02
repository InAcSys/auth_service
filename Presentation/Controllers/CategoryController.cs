using AuthService.Application.Services.Interfaces;
using AuthService.Domain.DTOs.Categories;
using AuthService.Domain.Entities.Concretes;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace AuthService.Presentation.Controllers
{
    [ApiController, Route("api/[controller]")]
    public class CategoryController(
        IService<Category, int> service,
        IMapper mapper
    ) : ControllerBase
    {
        private readonly IService<Category, int> _service = service;
        private readonly IMapper _mapper = mapper;

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var categories = await _service.GetAll(Guid.Empty);
            return Ok(categories);
        }

        [HttpGet("id/{id}/permissions")]
        public async Task<IActionResult> GetById([FromRoute] int id)
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
            var entity = _mapper.Map<Category>(category);
            var result = await _service.Create(entity, Guid.Empty);
            if (result is null)
            {
                return BadRequest("Category can not be created");
            }
            return Ok(result);
        }
    }
}
