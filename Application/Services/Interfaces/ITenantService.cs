namespace AuthService.Application.Services.Interfaces
{
    public interface ITenantService
    {
        Task<bool> Initialize(Guid id);
    }
}
