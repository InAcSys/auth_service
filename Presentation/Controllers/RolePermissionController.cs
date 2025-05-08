using AuthService.Application.Services.Interfaces;
using AuthService.Domain.DTOs.RolePermission;
using AuthService.Domain.Entities.Concretes;
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

        [HttpGet("role/{id}/permissions")]
        public async Task<IActionResult> GetPermissionsByRoleId(int id)
        {
            var result = await _service.GetPermissionsByRoleId(id);
            if (result is null)
            {
                return NotFound("Role not found");
            }
            return Ok(result);
        }

        [HttpPost("assign-permissions/role/{id}")]
        public async Task<IActionResult> Assign(int id, CreateRolePermissionDTO permissions)
        {
            try
            {
                var result = await _service.Assign(id, permissions);
                return Ok(result);
            }
            catch (InvalidOperationException exception)
            {
                return NotFound(exception.Message);
            }
        }

        [HttpDelete("revoke-permissions/role/{id}")]
        public async Task<IActionResult> RevokePermissionsToRole(int id, PermissionsDTO permissions)
        {
            try
            {
                var result = await _service.RevokePermissions(id, permissions);
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
