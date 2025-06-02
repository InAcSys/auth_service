using AuthService.Domain.DTOs.Roles;
using AuthService.Domain.Entities.Concretes;
using AutoMapper;

namespace AuthService.Presentation.Profiles
{
    public class RoleProfile : Profile
    {
        public RoleProfile()
        {
            CreateMap<Role, CreateRoleDTO>().ReverseMap();
            CreateMap<Role, UpdateRoleDTO>().ReverseMap();
        }
    }
}
