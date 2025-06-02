using AuthService.Domain.Entities.Abstracts;
using AuthService.Infrastructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace AuthService.Infrastructure.Repositories.Abstracts
{
    public abstract class Repository<T, TKey>(DbContext context) : IRepository<T, TKey> where T : Entity<TKey>
    {
        protected readonly DbContext _context = context;
        public async virtual Task<T?> Create(T entity)
        {
            await _context.Set<T>().AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async virtual Task<bool> Delete(TKey id, Guid tenantId)
        {
            var entity = await GetById(id, tenantId);
            if (entity is null)
            {
                return false;
            }
            entity.IsActive = false;
            entity.Deleted = DateTime.UtcNow;
            var result = await Update(id, entity, tenantId);
            return result is not null;
        }

        public virtual async Task<IEnumerable<T>> GetAll(Guid tenantId)
        {

            var query = _context.Set<T>().Where(x => x.IsActive);

            if (tenantId != Guid.Empty)
            {
                query = query.Where(x => x.TenantId == tenantId || x.TenantId == Guid.Empty);
            }

            var entities = await query
                .ToListAsync();

            return entities;
        }

        public virtual async Task<T?> GetById(TKey id, Guid tenantId)
        {
            if (EqualityComparer<TKey>.Default.Equals(id, default))
            {
                throw new ArgumentException("The ID cannot be the default value.", nameof(id));
            }

            var entity = await _context
                .Set<T>()
                .FirstOrDefaultAsync(r =>
                    Equals(r.Id, id) &&
                    (Equals(r.TenantId, tenantId) || Equals(r.TenantId, Guid.Empty)));

            if (entity is null)
            {
                return null;
            }

            return entity;
        }


        public async virtual Task<T> Update(TKey id, T entity, Guid tenantId)
        {
            if (EqualityComparer<TKey>.Default.Equals(id, default))
            {
                throw new ArgumentNullException(nameof(id));
            }

            var existingEntity = await GetById(id, tenantId);
            if (existingEntity is null)
            {
                throw new InvalidOperationException("The entity to update was not found.");
            }

            var keyProperty = typeof(T).GetProperty("Id");
            if (keyProperty != null && keyProperty.CanWrite)
            {
                keyProperty.SetValue(entity, keyProperty.GetValue(existingEntity));
            }

            existingEntity.Updated = DateTime.UtcNow;
            if (existingEntity.TenantId != Guid.Empty)
            {
                existingEntity.TenantId = tenantId;
            }

            _context.Entry(existingEntity).CurrentValues.SetValues(entity);
            await _context.SaveChangesAsync();

            return existingEntity;
        }

    }
}
