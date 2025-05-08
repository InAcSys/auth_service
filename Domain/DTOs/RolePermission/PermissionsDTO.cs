namespace AuthService.Domain.DTOs.RolePermission
{
    public class PermissionsDTO
    {
        public IEnumerable<int> permissionsId { get; set; } = new List<int>();
    }
}
