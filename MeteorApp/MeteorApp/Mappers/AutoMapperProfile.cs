using AutoMapper;
using MeteorApp.Models;
using Newtonsoft.Json;

namespace MeteorApp.Mappers
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Meteor, MeteorDB>()
                .ForMember(dest => dest.GeolocationJson, opt => opt.MapFrom(src => JsonConvert.SerializeObject(src.Geolocation)));
        }
    }
}
