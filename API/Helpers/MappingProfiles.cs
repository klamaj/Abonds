using AutoMapper;
using Core.DTOs;
using Core.Models;

namespace API.Helpers
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<ImageModel, ImageDto>()
                .ForMember(i => i.ClientId, m => m.MapFrom(i => i.ClientId))
                .ForMember(i => i.ImageId, m => m.MapFrom(i => i.Id))
                .ForMember(i => i.ImagePath, m => m.MapFrom<ImageUrlResolver>());
        }
    }
}