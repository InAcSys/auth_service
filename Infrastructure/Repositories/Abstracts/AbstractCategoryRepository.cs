using AuthService.Domain.DTOs.Categories;
using AuthService.Domain.Entities.Concretes;
using AuthService.Infrastructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace AuthService.Infrastructure.Repositories.Abstracts
{
    public class AbstractCategoryRepository(
        DbContext context
    ) : Repository<Category, int>(context), ICategoryRepository
    {
    }
}
