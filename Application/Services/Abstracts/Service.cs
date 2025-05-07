using FluentValidation;
using AuthService.Application.Services.Interfaces;
using AuthService.Infrastructure.Repositories.Interfaces;

namespace AuthService.Application.Services.Abstracts
{
    public class Service<T, TKey>
    (
        IValidator<T> validator,
        IRepository<T, TKey> repository
    ) : IService<T, TKey>
    {

        protected readonly IValidator<T> _validator = validator;
        protected readonly IRepository<T, TKey> _repository = repository;

        public virtual Task<T?> Create(T entity)
        {
            var result = _validator.Validate(entity);
            if (!result.IsValid)
            {
                throw new ValidationException(result.Errors);
            }
            var createdEntity = _repository.Create(entity);
            return createdEntity;
        }

        public virtual Task<bool> Delete(TKey id)
        {
            if (EqualityComparer<TKey>.Default.Equals(id, default))
            {
                throw new ArgumentNullException(nameof(id));
            }
            var result = _repository.Delete(id);
            return result;
        }

        public virtual Task<IEnumerable<T>> GetAll(int pageNumber, int pageSize)
        {
            var entities = _repository.GetAll(pageNumber, pageSize);
            return entities;
        }

        public virtual Task<T?> GetById(TKey id)
        {
            if (EqualityComparer<TKey>.Default.Equals(id, default))
            {
                throw new ArgumentNullException(nameof(id));
            }
            var entity = _repository.GetById(id);
            return entity;
        }

        public virtual Task<T> Update(TKey id, T entity)
        {
            if (EqualityComparer<TKey>.Default.Equals(id, default))
            {
                throw new ArgumentNullException(nameof(id));
            }
            if (entity is null)
            {
                throw new ArgumentNullException(nameof(entity));
            }
            var result = _validator.Validate(entity);
            if (!result.IsValid)
            {
                throw new ValidationException(result.Errors);
            }
            var updatedEntity = _repository.Update(id, entity);
            return updatedEntity;
        }
    }
}
