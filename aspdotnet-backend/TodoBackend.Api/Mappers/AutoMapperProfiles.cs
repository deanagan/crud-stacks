using AutoMapper;
using TodoBackend.Api.Data.Models;
using TodoBackend.Api.Data.ViewModels;

namespace TodoBackend.Api.Bindings
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<User, UserView>().ReverseMap();
            CreateMap<Role, RoleView>().ReverseMap();
            CreateMap<TodoView, Todo>().ForMember(dest => dest.AssigneeGuid,  opt => opt.MapFrom(src => src.Assignee.UniqueId));
        }
    }
}
