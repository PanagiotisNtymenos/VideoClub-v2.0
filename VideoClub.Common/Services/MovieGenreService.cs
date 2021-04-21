using System;
using System.Collections.Generic;
using System.Data.Entity;
using VideoClub.Core.Entities;
using VideoClub.Core.Enumerations;
using VideoClub.Core.Interfaces;
using VideoClub.Infrastructure.Data;

namespace VideoClub.Common.Services
{
    public class MovieGenreService : IMovieGenreService
    {
        private readonly VideoClubDbContext _context;

        public MovieGenreService()
        {
            _context = new VideoClubDbContext();
        }

        // Get

        // Add
        public void AddGenreToMovie(List<Genres> genres, Movie movie)
        {
            foreach (Genres genre in genres)
            {
                MovieGenre movieGenre = new MovieGenre
                {
                    Movie = movie,
                    Genre = (int)Enum.Parse(typeof(Genres), genre.ToString())
                };

                _context.MovieGenres.Add(movieGenre);
            }
            _context.SaveChangesAsync();
        }
    }
}
