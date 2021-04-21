using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using VideoClub.Core.Entities;
using VideoClub.Core.Interfaces;
using VideoClub.Infrastructure.Data;

namespace VideoClub.Common.Services
{
    public class MovieService : IMovieService
    {
        private readonly VideoClubDbContext _context;

        public MovieService()
        {
            _context = new VideoClubDbContext();
        }

        // Get
        public async Task<List<Movie>> GetMoviesByQuery(string q)
        {
            return await _context.Movies.Where(m => m.Title.Contains(q)).Include(m => m.Copies).Include(m => m.MovieGenres).ToListAsync();
        }

        public async Task<List<Movie>> GetAvailableMoviesByQuery(string q)
        {
            return await _context.Copies.Where(c => c.IsAvailable && c.Movie.Title.Contains(q)).Select(m => m.Movie).Distinct().ToListAsync();
        }

        public async Task<List<Movie>> GetAllMovies()
        {
            return await _context.Movies.Include(m => m.Copies).Include(m => m.MovieGenres).ToListAsync();
        }

        // Add
        public void AddMovie(Movie movie)
        {
            _context.Movies.Add(movie);
            _context.SaveChangesAsync();
        }

    }
}
