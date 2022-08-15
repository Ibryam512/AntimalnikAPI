using AntimalnikAPI.Common;
using AntimalnikAPI.DAL.Models;
using AntimalnikAPI.DTOs;
using AutoMapper;
using System;
using System.Collections.Generic;

namespace AntimalnikAPI.BLL.MappingConfiguration
{
    public class ApplicationProfile : Profile
    {
        public ApplicationProfile()
        {
            CreateMap<ApplicationUser, UserViewModel>();
            CreateMap<UserInputViewModel, ApplicationUser>()
                .ForMember(dest => dest.Posts, opt => opt.MapFrom(src => new List<Post>()))
                .ForMember(dest => dest.Role, opt => opt.MapFrom(src => RoleType.User));
            CreateMap<PostInputViewModel, Post>()
                .ForMember(dest => dest.CreationDate, opt => opt.MapFrom(src => DateTime.Now));
        }
    }
}
