namespace AuthService.Domain.DTOs.Permissions
{
    public class UpdatePermissionDTO
    {
        public string Name { get; set; } = "";
        public string Description { get; set; } = "";
        public string Path { get; set; } = "";
        public string Code { get; set; } = "";
        public int CategoryId { get; set; }
    }
}
