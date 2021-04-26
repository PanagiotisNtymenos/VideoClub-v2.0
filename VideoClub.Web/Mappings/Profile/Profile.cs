using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using VideoClub.Core.Entities;
using VideoClub.Web.Areas.Movies.Models;
using VideoClub.Web.Areas.Rentings.Models;

namespace VideoClub.Web.Mappings.Profile
{
    public class Profile : AutoMapper.Profile
    {
        public Profile()
        {
            CreateMap<MovieBindModel, Movie>();
            CreateMap<Movie, MovieViewModel>()
                .ForMember(desk => desk.Genres, opt => opt.Ignore())
                .ForMember(desk => desk.Availability, opt => opt.MapFrom(src => src.Copies.Count()));
            CreateMap<RentingBindModel, Renting>()
                .ForMember(desk => desk.RentingNotes, opt => opt.MapFrom(src => src.RentNotes));
        }
    }
}