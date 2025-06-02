namespace AuthService.Domain.DTOs.Permissions
{
    public class PermissionDTO
    {
        public int Id { get; set; }
        public string Name { get; set; } = "";
        public string Description { get; set; } = "";
        public string Path { get; set; } = "";
        public string Code { get; set; } = "";
    }
}
