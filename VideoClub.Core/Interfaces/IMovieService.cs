using System.Collections.Generic;
using System.Threading.Tasks;
using VideoClub.Core.Entities;

namespace VideoClub.Core.Interfaces
{
    public interface IMovieService
    {
        // Get
        Task<List<Movie>> GetMoviesByQuery(string q);
        Task<List<Movie>> GetAvailableMoviesByQuery(string q);
        Task<List<Movie>> GetAllMovies();

        // Add
        void AddMovie(Movie movie);
    }
}
