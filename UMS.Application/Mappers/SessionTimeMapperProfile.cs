using AutoMapper;
using UMS.Application.DTOs;
using UMS.Domain.Models;

namespace UMS.Application.Mappers;

public class SessionTimeMapperProfile : Profile
{
    public SessionTimeMapperProfile()
    {
        CreateMap<SessionTimeDTO, SessionTime>()
            .ForMember(dest => dest.StartTime,
                opt => opt.MapFrom(src => src.StartTime))
            .ForMember(dest => dest.EndTime,
                opt => opt.MapFrom(src => src.EndTime));
      
        CreateMap<SessionTime, SessionTimeDTO>()
            .ForMember(dest => dest.StartTime,
                opt => opt.MapFrom(src => src.StartTime))
            .ForMember(dest => dest.EndTime,
                opt => opt.MapFrom(src => src.EndTime));

    }
    
}