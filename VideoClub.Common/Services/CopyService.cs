using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VideoClub.Core.Entities;
using VideoClub.Core.Interfaces;
using VideoClub.Infrastructure.Data;

namespace VideoClub.Common.Services
{
    public class CopyService : ICopyService
    {
        private readonly VideoClubDbContext _context;

        public CopyService()
        {
            _context = new VideoClubDbContext();
        }


        // Get
        public async Task<List<Copy>> GetCopiesByTitleQuery(string q)
        {
            return await _context.Copies.Where(c => c.Movie.Title.Contains(q)).ToListAsync();
        }

        public async Task<Copy> GetAvailableCopyById(int movieId)
        {
            return await _context.Copies.Where(c => c.Movie.Id == movieId && c.IsAvailable).FirstAsync();
        }


        // Add
        public void AddCopies(int copies, Movie movie)
        {
            for (int i = 1; i <= copies; i++)
            {
                Copy copy = new Copy
                {
                    Movie = movie,
                    IsAvailable = true
                };

                _context.Copies.Add(copy);
            }
            _context.SaveChangesAsync();
        }

    }
}
