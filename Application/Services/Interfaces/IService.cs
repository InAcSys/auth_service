namespace AuthService.Application.Services.Interfaces
{
    public interface IService<T, TKey>
    {
        Task<IEnumerable<T>> GetAll(Guid tenantId);
        Task<T?> GetById(TKey id, Guid tenantId);
        Task<T?> Create(T entity, Guid tenantId);
        Task<T> Update(TKey id, T entity, Guid tenantId);
        Task<bool> Delete(TKey id, Guid tenantId);
    }
}
