using System.ComponentModel.DataAnnotations;
using AuthService.Application.Services.Interfaces;
using AuthService.Domain.DTOs.Permissions;
using AuthService.Domain.Entities.Concretes;
using Microsoft.AspNetCore.Mvc;

namespace AuthService.Presentation.Controllers
{
    [ApiController, Route("api/[controller]")]
    public class PermissionController(
        IService<Permission, int> service
    ) : ControllerBase
    {
        private readonly IService<Permission, int> _service = service;

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            var permissions = await _service.GetAll(pageNumber, pageSize, null);
            return Ok(permissions);
        }

        [HttpGet("id/{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var permission = await _service.GetById(id, Guid.Empty);
            if (permission is null)
            {
                return NotFound();
            }
            return Ok(permission);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreatePermissionDTO permission)
        {
            try
            {
                var newPermission = new Permission
                {
                    Name = permission.Name,
                    Description = permission.Description,
                    Path = permission.Path
                };
                var createdPermission = await _service.Create(newPermission, null);
                if (createdPermission is null)
                {
                    return BadRequest("Permission can not be create");
                }
                return CreatedAtAction(nameof(GetById), new { id = createdPermission.Id }, createdPermission);
            }
            catch (ValidationException exception)
            {
                return BadRequest(exception.Message);
            }
        }

        [HttpPut("id/{id}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdatePermissionDTO permission)
        {
            try
            {
                var updatedPermission = new Permission
                {
                    Name = permission.Name,
                    Description = permission.Description,
                    Path = permission.Path,
                    Updated = DateTime.UtcNow
                };
                var updatePermission = await _service.Update(id, updatedPermission, Guid.Empty);
                if (updatePermission is null)
                {
                    return BadRequest("Permission can not be udpate");
                }
                return Ok(updatePermission);
            }
            catch (ArgumentNullException)
            {
                return NotFound("Permission not found");
            }
            catch (ValidationException exception)
            {
                return BadRequest(exception.Message);
            }
        }

        [HttpDelete("id/{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            try
            {
                var result = await _service.Delete(id, Guid.Empty);
                if (!result)
                {
                    return BadRequest("Permission can not be delete");
                }
                return Ok($"Permission with id {id} was deleted");
            }
            catch (ArgumentNullException)
            {
                return BadRequest($"Permission with id {id} not found");
            }
        }
    }
}
