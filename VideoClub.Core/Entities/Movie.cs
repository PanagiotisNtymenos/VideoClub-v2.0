using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VideoClub.Core.Entities
{
    public class Movie
    {
        public Movie()
        {

        }

        public int Id { get; set; }

        public string Title { get; set; }

        public string Summary { get; set; }

        public virtual List<Copy> Copies { get; set; }

        public virtual List<MovieGenre> MovieGenres { get; set; }

    }
}
