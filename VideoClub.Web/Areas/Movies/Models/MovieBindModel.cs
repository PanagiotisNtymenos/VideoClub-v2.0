using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using VideoClub.Core.Enumerations;

namespace VideoClub.Web.Areas.Movies.Models
{
    public class MovieBindModel
    {
        [Required]
        public string Title { get; set; }

        [Required]
        public List<Genres> Genres { get; set; }

        [Required]
        public int copiesNumber { get; set; }

        [Required]
        public string Summary { get; set; }
    }
}