using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VideoClub.Core.Entities;
using VideoClub.Core.Enumerations;

namespace VideoClub.Core.Interfaces
{
    public interface IMovieGenreService
    {
        // Add
        void AddGenreToMovie(List<Genres> genres, Movie movie);
    }
}
