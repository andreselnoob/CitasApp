namespace CitasApp.Helpers;
using AutoMapper;
using CitasApp.DTOs;
using CitasApp.Entities;
using CitasApp.Extensions;

public class AutomapperProfiles : Profile
{
    public AutomapperProfiles()
    {
        CreateMap<AppUser, MemberDto>()
            .ForMember(dest => dest.PhotoUrl,
            opt => opt.MapFrom(src => src.Photos.FirstOrDefault(p => p.IsMain).Url))
           // .ForMember(dest => dest.Age, opt => opt.MapFrom(src => src.DameLaEdad()));
           .ForMember(dest => dest.Age,
           opt => opt.MapFrom(src => src.DateOfBirth.CalculateAge()));

        CreateMap<Photo, PhotoDto>();
    }
}
