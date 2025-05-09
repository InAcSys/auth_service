namespace AuthService.Domain.ObjectValue
{
    public class JWT
    {
        public JWTHeader JWTHeader { get; set; } = new();
        public JWTBody JWTBody { get; set; } = new();
    }
}
