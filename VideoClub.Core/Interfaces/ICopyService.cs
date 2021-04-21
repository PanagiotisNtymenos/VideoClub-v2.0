using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VideoClub.Core.Entities;

namespace VideoClub.Core.Interfaces
{
    public interface ICopyService
    {
        // Get
        Task<List<Copy>> GetCopiesByTitleQuery(string q);
        Task<Copy> GetAvailableCopyById(int movieId);

        // Add
        void AddCopies(int copies, Movie movie);
    }
}
