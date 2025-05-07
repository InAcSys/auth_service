using System.ComponentModel.DataAnnotations;
using AuthService.Application.Services.Interfaces;
using AuthService.Domain.DTOs.Roles;
using AuthService.Domain.Entities.Concretes;
using Microsoft.AspNetCore.Mvc;

namespace AuthService.Presentation.Controllers
{
    [ApiController, Route("api/[controller]")]
    public class RoleController(
        IService<Role, int> service
    ) : ControllerBase
    {
        private readonly IService<Role, int> _service = service;

        [HttpGet]
        public async Task<IActionResult> GetAll(int pageNumber = 1, int pageSize = 10)
        {
            var roles = await _service.GetAll(pageNumber, pageSize);
            return Ok(roles);
        }

        [HttpGet("id/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var role = await _service.GetById(id);
            if (role is null)
            {
                return NotFound();
            }
            return Ok(role);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateRoleDTO role)
        {
            try
            {
                var newRole = new Role
                {
                    Name = role.Name
                };
                var createdRole = await _service.Create(newRole);
                if (createdRole is null)
                {
                    return BadRequest("Role can not be create");
                }
                return CreatedAtAction(nameof(GetById), new { id = createdRole.Id }, createdRole);
            }
            catch (ValidationException exception)
            {
                return BadRequest(exception.Message);
            }
        }

        [HttpPut("id/{id}")]
        public async Task<IActionResult> Update(int id, UpdateRoleDTO role)
        {
            try
            {
                var updatedRole = new Role
                {
                    Name = role.Name,
                    Updated = DateTime.UtcNow
                };
                var updateRole = await _service.Update(id, updatedRole);
                if (updateRole is null)
                {
                    return BadRequest("Role can not be udpate");
                }
                return Ok(updateRole);
            }
            catch (ArgumentNullException)
            {
                return NotFound("Role not found");
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
                    return BadRequest("Role can not be delete");
                }
                return Ok($"Role with id {id} was deleted");
            }
            catch (ArgumentNullException)
            {
                return BadRequest($"Role with id {id} not found");
            }
        }
    }
}
