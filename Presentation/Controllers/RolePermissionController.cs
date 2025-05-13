using AuthService.Application.Decoders.JWT;
using AuthService.Application.Services.Interfaces;
using AuthService.Domain.DTOs.RolePermission;
using AuthService.Domain.ObjectValue;
using Microsoft.AspNetCore.Mvc;

namespace AuthService.Presentation.Controllers
{
    [ApiController, Route("api/[controller]")]
    public class RolePermissionController(
        IRolePermissionService service,
        IJWTDecoder decoder
    ) : ControllerBase
    {
        private readonly IJWTDecoder _decoder = decoder;

        private readonly IRolePermissionService _service = service;

        [HttpGet]
        public async Task<IActionResult> GetAll(
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 10,
            [FromHeader(Name = "Authorization")] string authorization = "",
            [FromQuery] Guid tenantId = default)
        {
            var tenantIdentity = tenantId;
            if (!string.IsNullOrEmpty(authorization))
            {
                tenantIdentity = _decoder.Decoder(authorization).TenantId;
            }
            var rolePermissions = await _service.GetAll(pageNumber, pageSize, tenantIdentity);
            return Ok(rolePermissions);
        }

        [HttpGet("role")]
        public async Task<IActionResult> GetById(
            [FromQuery] int id = 0,
            [FromHeader(Name = "Authorization")] string authorization = "",
            [FromQuery] Guid tenantId = default
        )
        {
            var tenantIdentity = tenantId;
            if (!string.IsNullOrEmpty(authorization))
            {
                tenantIdentity = _decoder.Decoder(authorization).TenantId;
            }
            var rolePermissions = await _service.GetById(id, tenantIdentity);
            if (rolePermissions is null)
            {
                return BadRequest($"Permissions list to role with id {id} not found");
            }
            return Ok(rolePermissions);
        }

        [HttpGet("role/permissions")]
        public async Task<IActionResult> GetPermissionsByRoleId(
            [FromHeader(Name = "Authorization")] string? authorization,
            [FromQuery] int roleId,
            [FromQuery] Guid tenantId)
        {
            var jwtBody = new JWTBody();
            if (!string.IsNullOrEmpty(authorization))
            {
                jwtBody = _decoder.Decoder(authorization);
            }
            var id = roleId == default ? jwtBody.RoleId : roleId;
            var tenant = tenantId == Guid.Empty ? jwtBody.TenantId : tenantId;
            var result = await _service.GetPermissionsByRoleId(id, tenant);
            if (result is null)
            {
                return NotFound("Role not found");
            }
            return Ok(result);
        }

        [HttpPost("assign-permissions/{id}")]
        public async Task<IActionResult> Assign(
            [FromHeader(Name = "Authorization")] string? authorization,
            [FromRoute] int id,
            [FromQuery] Guid tenantId,
            [FromBody] CreateRolePermissionDTO permissions
        )
        {
            try
            {
                var jwtBody = new JWTBody();
                if (!string.IsNullOrEmpty(authorization))
                {
                    jwtBody = _decoder.Decoder(authorization);
                }
                Guid tenantIdentity = tenantId == Guid.Empty ? jwtBody.TenantId : tenantId;
                var result = await _service.Assign(id, permissions, tenantIdentity);
                return Ok(result);
            }
            catch (InvalidOperationException exception)
            {
                return NotFound(exception.Message);
            }
        }

        [HttpDelete("revoke-permissions")]
        public async Task<IActionResult> RevokePermissionsToRole(
            [FromHeader(Name = "Authorization")] string? authorization,
            [FromQuery] int id,
            [FromQuery] Guid tenantId,
            [FromBody] PermissionsDTO permissions)
        {
            try
            {
                var jwtBody = new JWTBody();
                if (!string.IsNullOrEmpty(authorization))
                {
                    jwtBody = _decoder.Decoder(authorization);
                }
                int roleId = id == default ? jwtBody.RoleId : id;
                Guid tenantIdentity = tenantId == Guid.Empty ? jwtBody.TenantId : tenantId;
                var result = await _service.RevokePermissions(roleId, permissions, tenantIdentity);
                return Ok(result);
            }
            catch (InvalidOperationException exception)
            {
                return BadRequest(exception.Message);
            }
        }

        [HttpPost("verify-authorization")]
        public async Task<IActionResult> VerifyAuth([FromHeader(Name = "Authorization")] string authorization, [FromQuery] int permissionId)
        {
            var jwt = _decoder.Decoder(authorization);
            var verify = new HasPermissionDTO
            {
                RoleId = jwt.RoleId,
                PermissionId = permissionId,
                TenantId = jwt.TenantId
            };
            var result = await _service.HasPermission(verify);
            return Ok(result);
        }
    }
}
