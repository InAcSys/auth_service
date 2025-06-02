namespace AuthService.Domain.DTOs.Categories
{
    public class CreateCategoryDTO
    {
        public string Name { get; set; } = "";
        public int? ParentId { get; set; }
        public string Path { get; set; } = "";
    }
}
