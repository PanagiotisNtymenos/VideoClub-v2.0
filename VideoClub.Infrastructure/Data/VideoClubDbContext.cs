using Microsoft.AspNet.Identity.EntityFramework;
using VideoClub.Core.Entities;

namespace VideoClub.Infrastructure.Data
{
        public class VideoClubDbContext : IdentityDbContext<User>
    {
        public VideoClubDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static VideoClubDbContext Create()
        {
            return new VideoClubDbContext();
        }
    }
}
