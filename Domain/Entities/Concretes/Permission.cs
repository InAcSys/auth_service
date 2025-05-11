using AuthService.Domain.Entities.Abstracts;

namespace AuthService.Domain.Entities.Concretes
{
    public class Permission : Entity<int>
    {
        public string Name { get; set; } = "";
        public string Description { get; set; } = "";
        public string Path { get; set; } = "";
        public int? CategoryId { get; set; }
    }
}
