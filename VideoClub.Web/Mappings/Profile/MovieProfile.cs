using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using VideoClub.Core.Entities;
using VideoClub.Web.Areas.Movies.Models;

namespace VideoClub.Web.Mappings.Profile
{
    public class MovieProfile : AutoMapper.Profile
    {
        public MovieProfile()
        {
            CreateMap<MovieBindModel, Movie>(MemberList.None);
        }
    }
}