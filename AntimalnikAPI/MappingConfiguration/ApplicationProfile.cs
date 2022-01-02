using AntimalnikAPI.Enums;
using AntimalnikAPI.Models;
using AntimalnikAPI.ViewModels;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AntimalnikAPI.MappingConfiguration
{
    public class ApplicationProfile : Profile
    {
        public ApplicationProfile()
        {
            CreateMap<ApplicationUser, UserViewModel>();
            CreateMap<UserInputViewModel, ApplicationUser>()
                .ForMember(dest => dest.Posts, opt => opt.MapFrom(src => new List<Post>()))
                .ForMember(dest => dest.Role, opt => opt.MapFrom(src => RoleType.User));
        }
    }
}
