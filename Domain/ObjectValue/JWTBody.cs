namespace AuthService.Domain.ObjectValue
{
    public class JWTBody
    {
        public Guid UserId { get; set; }
        public int RoleId { get; set; }
        public Guid TenantId { get; set; }
    }
}
