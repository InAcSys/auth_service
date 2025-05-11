using AuthService.Application.Decoders.JWT;
using AuthService.Application.Services.Interfaces;
using AuthService.Domain.DTOs.RolePermission;
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
        public async Task<IActionResult> GetAll(int pageNumber = 1, int pageSize = 10)
        {
            var rolePermissions = await _service.GetAll(pageNumber, pageSize);
            return Ok(rolePermissions);
        }

        [HttpGet("id/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var rolePermissions = await _service.GetById(id);
            if (rolePermissions is null)
            {
                return BadRequest($"Permissions list to role with id {id} not found");
            }
            return Ok(rolePermissions);
        }

        [HttpGet("role/permissions/{id}")]
        public async Task<IActionResult> GetPermissionsByRoleId()
        {
            string? jwt = Request.Headers["Authorization"];
            if (string.IsNullOrEmpty(jwt))
            {
                return BadRequest();
            }
            var jwtBody = _decoder.Decoder(jwt);
            var result = await _service.GetPermissionsByRoleId(jwtBody.RoleId, jwtBody.TenantId);
            if (result is null)
            {
                return NotFound("Role not found");
            }
            return Ok(result);
        }

        [HttpPost("assign-permissions")]
        public async Task<IActionResult> Assign([FromQuery] int? id, [FromQuery] Guid? tenantId, [FromBody] CreateRolePermissionDTO permissions)
        {
            try
            {
                string? jwt = Request.Headers["Authorization"];
                if (string.IsNullOrEmpty(jwt))
                {
                    return BadRequest();
                }
                var jwtBody = _decoder.Decoder(jwt);
                int roleId = id is null ? jwtBody.RoleId : (int)id;
                Guid tenantIdentity = tenantId is null ? jwtBody.TenantId : (Guid)tenantId;
                var result = await _service.Assign(roleId, permissions, tenantIdentity);
                return Ok(result);
            }
            catch (InvalidOperationException exception)
            {
                return NotFound(exception.Message);
            }
        }

        [HttpDelete("revoke-permissions/role/{id}")]
        public async Task<IActionResult> RevokePermissionsToRole(int? id, PermissionsDTO permissions)
        {
            try
            {
                string? jwt = Request.Headers["Authorization"];
                if (string.IsNullOrEmpty(jwt))
                {
                    return BadRequest();
                }
                var jwtBody = _decoder.Decoder(jwt);
                int roleId = id is null ? jwtBody.RoleId : (int)id;
                var result = await _service.RevokePermissions(roleId, permissions, jwtBody.TenantId);
                return Ok(result);
            }
            catch (InvalidOperationException exception)
            {
                return BadRequest(exception.Message);
            }
        }

        [HttpPost("verify-authorization")]
        public async Task<IActionResult> VerifyAuth(HasPermissionDTO verify)
        {
            var result = await _service.HasPermission(verify);
            return Ok(result);
        }
    }
}
