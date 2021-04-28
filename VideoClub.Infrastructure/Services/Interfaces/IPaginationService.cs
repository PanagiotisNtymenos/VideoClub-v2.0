using VideoClub.Infrastructure.Models.DTOs;

namespace VideoClub.Infrastructure.Services.Interfaces
{
    public interface IPaginationService
    {
        PaginationDto GetMoviesPagination(int? current, int? size);
    }
}
