namespace AuthService.Domain.DTOs.RolePermission
{
    public class RolePermissionsDTO
    {
        public RoleDTO Role { get; set; } = new();
        public IEnumerable<PermissionDTO> Permissions { get; set; } = new List<PermissionDTO>();
    }
}
