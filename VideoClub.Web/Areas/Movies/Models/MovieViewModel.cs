using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using VideoClub.Core.Entities;
using VideoClub.Core.Enumerations;

namespace VideoClub.Web.Areas.Movies.Models
{
    public class MovieViewModel
    {

        [Required]
        public Movie Movie { get; set; }

        [Required]
        public List<Genres> Genres { get; set; }

        [Required]
        public int Availabilty { get; set; }

        public MovieViewModel(Movie Movie, List<Genres> Genres, int Availabilty)
        {
            this.Movie = Movie;
            this.Genres = Genres;
            this.Availabilty = Availabilty;
        }
    }
}