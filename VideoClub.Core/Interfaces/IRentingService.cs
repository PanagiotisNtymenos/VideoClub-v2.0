using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VideoClub.Core.Entities;

namespace VideoClub.Core.Interfaces
{
    public interface IRentingService
    {
        // Get 
        Task<List<Renting>> GetAllActiveRentings();
        Task<Renting> GetRentingById(int rentingId);
        Task<List<Renting>> GetUserRentings(string usernme);

        // Add
        Task AddRenting(Renting renting, User user, Copy copy);

        // Edit
        Task ReturnRenting(Renting renting);
    }
}
