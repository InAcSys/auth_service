using AuthService.Domain.DTOs.Permissions;

namespace AuthService.Domain.DTOs.Categories
{
    public class PermissionsByCategoryDTO
    {
        public int Id { get; set; }
        public string Name { get; set; } = "";
        public string Path { get; set; } = "";
        public IEnumerable<PermissionsByCategoryDTO>? SubCategories { get; set; } = new List<PermissionsByCategoryDTO>();
        public IEnumerable<PermissionDTO>? Permissions { get; set; } = new List<PermissionDTO>();
    }
}
