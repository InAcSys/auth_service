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

        public async virtual Task<bool> Delete(TKey id)
        {
            var entity = await _context.Set<T>().FindAsync(id);
            if (entity is null)
            {
                return false;
            }
            entity.IsActive = false;
            entity.Deleted = DateTime.UtcNow;
            var result = await Update(id, entity);
            return result is not null;
        }

        public async virtual Task<IEnumerable<T>> GetAll(int pageNumber, int pageSize)
        {
            if (pageNumber < 1)
            {
                throw new ArgumentOutOfRangeException(nameof(pageNumber), "Page number must be greater than or equal to 1.");
            }
            if (pageSize < 1)
            {
                throw new ArgumentOutOfRangeException(nameof(pageSize), "Page size must be greater than or equal to 1.");
            }

            var skip = (pageNumber - 1) * pageSize;
            var take = pageSize;

            var entities = await _context.Set<T>()
                .Where(x => x.IsActive)
                .Skip(skip)
                .Take(take)
                .ToListAsync();

            return entities;
        }

        public virtual async Task<T?> GetById(TKey id)
        {
            if (EqualityComparer<TKey>.Default.Equals(id, default))
            {
                throw new ArgumentException("The ID cannot be the default value.", nameof(id));
            }

            var entity = await _context.Set<T>().FindAsync(id);

            if (entity is null)
            {
                return null;
            }

            return entity;
        }


        public async virtual Task<T> Update(TKey id, T entity)
        {
            if (EqualityComparer<TKey>.Default.Equals(id, default))
            {
                throw new ArgumentNullException(nameof(id));
            }

            var existingEntity = await _context.Set<T>().FindAsync(id);
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

            _context.Entry(existingEntity).CurrentValues.SetValues(entity);
            await _context.SaveChangesAsync();

            return existingEntity;
        }

    }
}
