using AutoMapper;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Models.DTOs;
using Models.Models;

namespace TeamFury_API.MappingProfiles;

public class UserConfig : Profile
{
    public UserConfig()
    {
        CreateMap<User, UserCreateDTO>().ReverseMap();
        CreateMap<User, UserUpdateDTO>().ReverseMap();
        CreateMap<User, UserViewDTO>().ReverseMap();
        CreateMap<UserViewDTO, User>().ForAllMembers(opt =>
            opt.MapFrom(inner => inner));
    }
}