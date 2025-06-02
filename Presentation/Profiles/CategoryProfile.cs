using AuthService.Domain.DTOs.Categories;
using AuthService.Domain.Entities.Concretes;
using AutoMapper;

namespace AuthService.Presentation.Profiles
{
    public class CategoryProfile : Profile
    {
        public CategoryProfile()
        {
            CreateMap<Category, CreateCategoryDTO>().ReverseMap();
        }
    }
}
