using AuthService.Domain.DTOs.RolePermission;

namespace AuthService.Domain.DTOs.Categories
{
    public class CategoryDTO
    {
        public int Id { get; set; }
        public string Name { get; set; } = "";
        public IEnumerable<PermissionDTO> Permissions { get; set; } = new List<PermissionDTO>();
    }
}
