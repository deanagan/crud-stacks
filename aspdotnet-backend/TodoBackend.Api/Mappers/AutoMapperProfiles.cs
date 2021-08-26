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
                        Name = src.RoleName,
                        Description = src.Description
                    }));

        }
    }
}