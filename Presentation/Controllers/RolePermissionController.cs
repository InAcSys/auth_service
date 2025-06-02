using AuthService.Application.Services.Interfaces;
using AuthService.Domain.DTOs.RolePermission;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace AuthService.Presentation.Controllers
{
    [ApiController, Route("api/[controller]")]
    public class RolePermissionController(
        IRolePermissionService service
    ) : ControllerBase
    {

        private readonly IRolePermissionService _service = service;

        [HttpGet]
        public async Task<IActionResult> GetAll(
            [FromQuery] Guid tenantId = default
        )
        {
            var rolePermissions = await _service.GetAll(tenantId);
            return Ok(rolePermissions);
        }

        [HttpGet("role")]
        public async Task<IActionResult> GetById(
            [FromQuery] int id = 0,
            [FromQuery] Guid tenantId = default
        )
        {
            var rolePermissions = await _service.GetById(id, tenantId);
            if (rolePermissions is null)
            {
                return BadRequest($"Permissions list to role with id {id} not found");
            }
            return Ok(rolePermissions);
        }

        [HttpGet("role/permissions")]
        public async Task<IActionResult> GetPermissionsByRoleId(
            [FromQuery] int roleId,
            [FromQuery] Guid tenantId)
        {
            var result = await _service.GetPermissionsByRoleId(roleId, tenantId);
            if (result is null)
            {
                return NotFound("Role not found");
            }
            return Ok(result);
        }

        [HttpPost("assign-permissions/{id}")]
        public async Task<IActionResult> Assign(
            [FromRoute] int id,
            [FromQuery] Guid tenantId,
            [FromBody] CreateRolePermissionDTO permissions
        )
        {
            try
            {
                var result = await _service.Assign(id, permissions, tenantId);
                return Ok(result);
            }
            catch (InvalidOperationException exception)
            {
                return NotFound(exception.Message);
            }
        }

        [HttpDelete("revoke-permissions")]
        public async Task<IActionResult> RevokePermissionsToRole(
            [FromQuery] int roleId,
            [FromQuery] Guid tenantId,
            [FromBody] PermissionsDTO permissions)
        {
            try
            {
                var result = await _service.RevokePermissions(roleId, permissions, tenantId);
                return Ok(result);
            }
            catch (InvalidOperationException exception)
            {
                return BadRequest(exception.Message);
            }
        }

        [HttpPost("verify-authorization")]
        public async Task<IActionResult> VerifyAuth([FromBody] HasPermissionDTO verify)
        {
            var result = await _service.HasPermission(verify);
            return Ok(result);
        }
    }
}
