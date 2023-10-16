using AutoMapper;
using Models.DTOs;
using Models.Models;

namespace TeamFury_API.MappingProfiles;

public class RequestTypeConfig : Profile
{
    public RequestTypeConfig()
    {
        CreateMap<RequestType, RequestTypeDto>().ReverseMap();
    }
}