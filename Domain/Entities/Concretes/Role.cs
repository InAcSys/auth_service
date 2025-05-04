using AuthService.Domain.Entities.Abstracts;

namespace AuthService.Domain.Entities.Concretes
{
    public class Role : Entity<int>
    {
        public string Name { get; set; } = "";
    }
}
