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
            CreateMap<User, UserViewModel>().ReverseMap();
            CreateMap<Role, RoleViewModel>().ReverseMap();
            CreateMap<TodoViewModel, Todo>().ForMember(dest => dest.AssigneeGuid,
                opt => opt.MapFrom(src => src.Assignee != null ? src.Assignee.UniqueId : Guid.Empty));

            CreateMap<RegisterViewModel, UserViewModel>().ConstructUsing(rv => new UserViewModel()
            {
                UserName = rv.UserName,
                FirstName = rv.FirstName,
                LastName = rv.LastName,
                Email = rv.Email,
                Role = new RoleViewModel()
                {
                    UniqueId = rv.RoleUniqueId
                }
            });
        }
    }
}
