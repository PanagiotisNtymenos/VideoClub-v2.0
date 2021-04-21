using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using VideoClub.Core.Entities;
using VideoClub.Core.Enumerations;

namespace VideoClub.Web.Areas.Movies.Models
{
    public class MovieBindModel
    {
        [Required]
        public Movie Movie { get; set; }

        [Required]
        public List<Genres> Genres { get; set; }

        [Required]
        public int copiesNumber { get; set; }
    }
}