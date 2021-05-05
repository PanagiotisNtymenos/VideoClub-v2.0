using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using VideoClub.Core.Entities;
using VideoClub.Core.Enumerations;

namespace VideoClub.Web.Areas.Movies.Models
{
    public class MovieViewModel
    {

        public int Id { get; set; }

        public string Poster { get; set; }

        public string Title { get; set; }

        public string Summary { get; set; }

        public int Availability { get; set; }

        public List<MovieGenre> MovieGenres { get; set; }

        public List<Genres> Genres { get; set; }

        public MovieViewModel() { }
    }
}