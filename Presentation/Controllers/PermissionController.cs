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
        public async Task<IActionResult> GetAll(int pageNumber = 1, int pageSize = 10)
        {
            var permissions = await _service.GetAll(pageNumber, pageSize);
            return Ok(permissions);
        }

        [HttpGet("id/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var permission = await _service.GetById(id);
            if (permission is null)
            {
                return NotFound();
            }
            return Ok(permission);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreatePermissionDTO permission)
        {
            try
            {
                var newPermission = new Permission
                {
                    Name = permission.Name,
                    Description = permission.Description,
                    Path = permission.Path
                };
                var createdPermission = await _service.Create(newPermission);
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
        public async Task<IActionResult> Update(int id, UpdatePermissionDTO permission)
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
                var updatePermission = await _service.Update(id, updatedPermission);
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
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var result = await _service.Delete(id);
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
