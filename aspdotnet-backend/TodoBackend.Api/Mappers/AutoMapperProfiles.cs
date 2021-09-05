using AutoMapper;
using TodoBackend.Api.Data.Dtos;
using TodoBackend.Api.Data.Models;

namespace TodoBackend.Api.Bindings
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<UserDto, UserView>()
                .ForMember(dest => dest.Role, opt => opt.MapFrom(src => new Role
                    {
                        Id = src.RoleId,
                        UniqueId = src.RoleUniqueId,
                        Kind = src.RoleKind,
                        Created = src.RoleCreated,
                        Updated = src.RoleUpdated,
                        Description = src.RoleDescription
                    }));
            CreateMap<User, UserDto>()
                .ForMember(dest => dest.RoleId, opt => opt.MapFrom(src => src.Role.Id));

            CreateMap<Role, RoleDto>().ReverseMap();
        }
    }
}