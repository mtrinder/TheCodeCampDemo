using System;
using AutoMapper;
using TheCodeCamp.Data;
using TheCodeCamp.Data.Models;

namespace TheCodeCamp.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Camp, CampModel>()
                .ForMember(c => c.Venue, opt => opt.MapFrom(m => m.Location.VenueName))
                .ReverseMap();
        }
    }
}
