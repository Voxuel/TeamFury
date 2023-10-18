using AutoMapper;
using Models.DTOs;
using Models.Models;

namespace TeamFury_API.MappingProfiles;

public class RequestLogConfig : Profile
{
    public RequestLogConfig()
    {
        CreateMap<Request, RequestLogEntityDTO>().ReverseMap();
        CreateMap<RequestLog, RequestLogDTO>().ReverseMap();
        CreateMap<RequestLogEntityDTO, RequestLog>().ForAllMembers(r =>
            r.MapFrom(inner => inner));
    }
}