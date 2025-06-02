using System.ComponentModel.DataAnnotations;
using AuthService.Application.Services.Interfaces;
using AuthService.Domain.DTOs.Permissions;
using AuthService.Domain.Entities.Concretes;
using AutoMapper;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace AuthService.Presentation.Controllers
{
    [ApiController, Route("api/[controller]")]
    public class PermissionController(
        IPermissionService service,
        IMapper mapper
    ) : ControllerBase
    {
        private readonly IPermissionService _service = service;
        private readonly IMapper _mapper = mapper;

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var permissions = await _service.GetAll(Guid.Empty);
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
                var entity = _mapper.Map<Permission>(permission);
                var createdPermission = await _service.Create(entity, Guid.Empty);
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
                var entity = _mapper.Map<Permission>(permission);
                var updatePermission = await _service.Update(id, entity, Guid.Empty);
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

        [HttpGet("role")]
        public async Task<IActionResult> GetPermissionsByRole([FromQuery] int roleId, [FromQuery] Guid tenantId)
        {
            try
            {
                var result = await _service.GetPermissionsByRole(roleId, tenantId);
                if (!result.Any())
                {
                    return NotFound("This role has no permissions assigned");
                }
                return Ok(result);
            }
            catch (ArgumentException exception)
            {
                return BadRequest(exception.Message);
            }

        }
    }
}
