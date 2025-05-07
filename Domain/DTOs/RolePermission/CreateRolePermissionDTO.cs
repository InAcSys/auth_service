namespace AuthService.Domain.DTOs.RolePermission
{
    public class CreateRolePermissionDTO
    {
        public IEnumerable<int> Permissions { get; set; } = new List<int>();
    }
}
