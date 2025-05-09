namespace AuthService.Domain.DTOs.RolePermission
{
    public class HasPermissionDTO
    {
        public string JWT { get; set; } = "";
        public int PermissionId { get; set;}
    }
}
