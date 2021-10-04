using System;
using AutoMapper;
using TodoBackend.Api.Data.Models;
using TodoBackend.Api.Data.ViewModels;

namespace TodoBackend.Api.Bindings
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            // Note: Map<ObjectFrom, ObjectTo>
            CreateMap<User, UserView>().ReverseMap();
            CreateMap<Role, RoleView>().ReverseMap();
            CreateMap<TodoView, Todo>().ForMember(dest => dest.AssigneeGuid,
                opt => opt.MapFrom(src => src.Assignee != null ? src.Assignee.UniqueId : Guid.Empty));

            CreateMap<RegisterView, UserView>().ConstructUsing(rv => new UserView()
            {
                FirstName = rv.FirstName,
                LastName = rv.LastName,
                Email = rv.Email,
                Role = new RoleView()
                {
                    UniqueId = rv.RoleUniqueId
                }
            });
        }
    }
}
