using AuthService.Domain.DTOs.RolePermission;
using AuthService.Domain.Entities.Concretes;
using AutoMapper;

namespace AuthService.Presentation.Profiles
{
    public class RolePermissionProfile : Profile
    {
        public RolePermissionProfile()
        {
            CreateMap<RolePermission, CreateRolePermissionDTO>().ReverseMap();
            CreateMap<RolePermission, HasPermissionDTO>().ReverseMap();
        }
    }
}
