using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VideoClub.Infrastructure.Services.Interfaces
{
    public interface IOMDbAPIService
    {
        Task<string> GetMovieArtworkByTitle(string q);
    }
}
