using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VideoClub.Core.Entities
{
    public class Movie
    {
        public Movie(string title, string summary)
        {
            Title = title;
            Summary = summary;
            Copies = new List<Copy>();
            MovieGenres = new List<MovieGenre>();
        }

        public Movie()
        {
            Copies = new List<Copy>();
            MovieGenres = new List<MovieGenre>();
        }

        public int Id { get; set; }

        public string Title { get; set; }

        public string Summary { get; set; }

        public virtual List<Copy> Copies { get; set; }

        public virtual List<MovieGenre> MovieGenres { get; set; }

    }
}
