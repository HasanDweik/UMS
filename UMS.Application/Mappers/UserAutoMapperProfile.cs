using AutoMapper;
using UMS.Application.DTOs;
using UMS.Domain.Models;

namespace UMS.Application.Mappers;

public class UserAutoMapperProfile:Profile
{
    public UserAutoMapperProfile()
    {
        CreateMap<UserDTO, User>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
            .ForMember(dest => dest.Subscriber, opt => opt.MapFrom(src => src.Subscriber))
            .ForMember(dest => dest.RoleId, opt => opt.MapFrom(src => src.RoleId));
        CreateMap<User, UserDTO>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
            .ForMember(dest => dest.Subscriber, opt => opt.MapFrom(src => src.Subscriber))
            .ForMember(dest => dest.RoleId, opt => opt.MapFrom(src => src.RoleId));
    }
}