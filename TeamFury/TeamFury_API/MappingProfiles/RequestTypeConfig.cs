using AutoMapper;
using Models.DTOs;
using Models.Models;

namespace TeamFury_API.MappingProfiles;

public class RequestTypeConfig : Profile
{
    public RequestTypeConfig()
    {
        CreateMap<RequestType, RemaningCorrectDTO>().ReverseMap();
        CreateMap<RemaningCorrectDTO, RequestType>().ForAllMembers(opt => 
            opt.MapFrom(d => d));
        CreateMap<RequestType, RemainingLeaveDaysDTO>().ForMember(dest =>
                dest.LeaveType, opt =>
                opt.MapFrom(src => src.Name))
        .ForMember(dest =>
            dest.DaysLeft, opt =>
            opt.MapFrom(src => src.MaxDays));
        
        CreateMap<RequestType, RequestTypeDto>().ReverseMap();
    }
}