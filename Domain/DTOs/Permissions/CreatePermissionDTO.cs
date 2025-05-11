namespace AuthService.Domain.DTOs.Permissions
{
    public class CreatePermissionDTO
    {
        public string Name { get; set; } = "";
        public string Description { get; set; } = "";
        public string Path { get; set; } = "/";
        public int CategoryId { get; set; }
    }
}
