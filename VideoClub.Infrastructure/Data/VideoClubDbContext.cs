using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;
using VideoClub.Core.Entities;

namespace VideoClub.Infrastructure.Data
{
    public class VideoClubDbContext : IdentityDbContext<User>
    {
        public VideoClubDbContext()
            : base("VideoClubDbContext", throwIfV1Schema: false)
        {
        }

        public DbSet<Movie> Movies { get; set; }
        public DbSet<Copy> Copies { get; set; }
        public DbSet<Renting> Rentings { get; set; }
        public DbSet<MovieGenre> MovieGenres { get; set; }
        public DbSet<UserGenre> UserGenres { get; set; }

        public static VideoClubDbContext Create()
        {
            return new VideoClubDbContext();
        }
    }
}
