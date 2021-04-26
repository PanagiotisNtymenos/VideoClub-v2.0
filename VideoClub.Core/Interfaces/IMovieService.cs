using System.Collections.Generic;
using System.Threading.Tasks;
using VideoClub.Core.Entities;
using VideoClub.Core.Enumerations;

namespace VideoClub.Core.Interfaces
{
    public interface IMovieService
    {
        // Get
        Task<List<Movie>> GetMoviesByQuery(string q);
        Task<List<Movie>> GetAvailableMoviesByQuery(string q);
        Task<List<Movie>> GetAllAvailableMovies();
        Task<List<Movie>> GetAllMovies();

        // Add
        Task AddMovie(Movie movie, List<Genres> genres, int copiesNumber);
    }
}
