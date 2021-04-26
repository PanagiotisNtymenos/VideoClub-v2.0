using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using VideoClub.Core.Entities;
using VideoClub.Core.Enumerations;
using VideoClub.Core.Interfaces;
using VideoClub.Infrastructure.Data;

namespace VideoClub.Common.Services
{
    public class MovieService : IMovieService
    {
        private readonly VideoClubDbContext _context;

        public MovieService(VideoClubDbContext context)
        {
            _context = context;
        }

        // Get
        public async Task<Movie> GetMovieById(int Id)
        {
            return await _context.Movies.Where(m => m.Id == Id).Include(m => m.Copies).Include(m => m.MovieGenres).FirstAsync();
        }

        public async Task<List<Movie>> GetMoviesByQuery(string q)
        {
            return await _context.Movies.Where(m => m.Title.Contains(q)).Include(m => m.Copies).Include(m => m.MovieGenres).ToListAsync();
        }

        public async Task<List<Movie>> GetAvailableMoviesByQuery(string q)
        {
            return await _context.Copies.Where(c => c.IsAvailable && c.Movie.Title.Contains(q)).Select(m => m.Movie).Distinct().ToListAsync();
        }

        public async Task<List<Movie>> GetAllAvailableMovies()
        {
            return await _context.Movies.Include(i => i.Copies).Include(i => i.MovieGenres).ToListAsync();
        }

        public async Task<List<Movie>> GetAllMovies()
        {
            return await _context.Movies.Include(m => m.Copies).Include(m => m.MovieGenres).ToListAsync();
        }

        // Add
        public async Task AddMovie(Movie movie, List<Genres> genres, int copies)
        {
            foreach (Genres genre in genres)
            {
                movie.MovieGenres.Add(new MovieGenre(movie, (int)Enum.Parse(typeof(Genres), genre.ToString())));
            }

            for (int i = 1; i <= copies; i++)
            {
                movie.Copies.Add(new Copy(movie));
            }

            _context.Movies.Add(movie);
            await _context.SaveChangesAsync();
        }

    }
}
