using AutoMapper;
using Models.DTOs;
using Models.Models;

namespace TeamFury_API.MappingProfiles
{
    public class RequestConfig : Profile
    {
        public RequestConfig()
        {
            CreateMap<Request, RequestUpdateDTO>().ReverseMap();
            CreateMap<Request, RequestCreateDTO>().ReverseMap();
        }
    }
}
