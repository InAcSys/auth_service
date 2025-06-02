using AuthService.Domain.DTOs.Permissions;
using AuthService.Domain.Entities.Concretes;
using AutoMapper;

namespace AuthService.Presentation.Profiles
{
    public class PermissionProfile : Profile
    {
        public PermissionProfile()
        {
            CreateMap<Permission, CreatePermissionDTO>().ReverseMap();
            CreateMap<Permission, PermissionDTO>().ReverseMap();
            CreateMap<Permission, UpdatePermissionDTO>().ReverseMap();
        }
    }
}
