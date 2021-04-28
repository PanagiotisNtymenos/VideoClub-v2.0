using VideoClub.Infrastructure.Models.DTOs;
using VideoClub.Infrastructure.Services.Interfaces;

namespace VideoClub.Infrastructure.Services.Implementations
{
    public class PaginationService : IPaginationService
    {
        public PaginationDto GetMoviesPagination(int? current, int? size)
        {
            return new PaginationDto(current, size);
        }
    }
}
