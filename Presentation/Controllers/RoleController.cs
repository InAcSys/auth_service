using System.ComponentModel.DataAnnotations;
using AuthService.Application.Services.Interfaces;
using AuthService.Domain.DTOs.Roles;
using AuthService.Domain.Entities.Concretes;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace AuthService.Presentation.Controllers
{
    [ApiController, Route("api/[controller]")]
    public class RoleController(
        IService<Role, int> service,
        IMapper mapper
    ) : ControllerBase
    {
        private readonly IService<Role, int> _service = service;
        private readonly IMapper _mapper = mapper;

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] Guid tenantId = default)
        {
            var roles = await _service.GetAll(tenantId);
            return Ok(roles);
        }

        [HttpGet("id")]
        public async Task<IActionResult> GetById([FromQuery] Guid tenantId, [FromQuery] int roleId)
        {
            var role = await _service.GetById(roleId, tenantId);
            if (role is null)
            {
                return NotFound();
            }
            return Ok(role);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateRoleDTO role, [FromQuery] Guid tenantId)
        {
            try
            {
                var entity = _mapper.Map<Role>(role);
                entity.TenantId = tenantId;
                var createdRole = await _service.Create(entity, tenantId);
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

        [HttpPut]
        public async Task<IActionResult> Update([FromQuery] int id, [FromBody] UpdateRoleDTO role, [FromQuery] Guid tenantId)
        {
            try
            {
                var entity = _mapper.Map<Role>(role);
                entity.Updated = DateTime.UtcNow;
                var updateRole = await _service.Update(id, entity, tenantId);
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

        [HttpDelete]
        public async Task<IActionResult> Delete([FromQuery] int id, [FromQuery] Guid tenantId)
        {
            try
            {
                var result = await _service.Delete(id, tenantId);
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
