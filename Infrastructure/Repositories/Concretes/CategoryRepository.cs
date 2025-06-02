using AuthService.Domain.Entities.Concretes;
using AuthService.Infrastructure.Repositories.Abstracts;
using Microsoft.EntityFrameworkCore;

namespace AuthService.Infrastructure.Repositories.Concretes
{
    public class CategoryRepository(DbContext context) : AbstractCategoryRepository(context)
    {
    }
}
