namespace AuthService.Domain.DTOs.RolePermission
{
    public class VerifyPermissionDTO
    {
        public int RoleId { get; set; }
        public int PermissionId { get; set; }
        public int TenantId { get; set; }
    }
}
